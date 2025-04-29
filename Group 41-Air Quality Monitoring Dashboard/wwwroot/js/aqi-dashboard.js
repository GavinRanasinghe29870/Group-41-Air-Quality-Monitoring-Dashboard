let aqiSensors = [];
let aqiMarkers = {};
let aqiMap;

async function fetchSensors() {
    try {
        const res = await fetch('/Dashboards/GetSensors');
        if (!res.ok) {
            throw new Error('Failed to fetch sensors');
        }

        const data = await res.json();
        aqiSensors = data;

        const select = document.getElementById('sensorSelect');
        select.innerHTML = ''; // Clear existing options

        if (data.length > 0) {
            data.forEach(sensor => {
                const option = document.createElement('option');
                option.value = sensor.sensorId;  // match lowercase
                option.textContent = sensor.location;
                select.appendChild(option);
            });
        } else {
            const option = document.createElement('option');
            option.disabled = true;
            option.selected = true;
            option.textContent = 'No Sensors Available';
            select.appendChild(option);
        }

        loadMap(); // Always load map even if 0 sensors
    } catch (error) {
        console.error('Error fetching sensors:', error);
        loadMap(); // Try to show empty map anyway
    }
}


function aqiGetColor(aqi) {
    if (aqi <= 50) return 'green';
    if (aqi <= 100) return 'yellow';
    if (aqi <= 150) return '#FF8C00';
    return 'red';
}

function loadMap() {
    if (aqiMap) {
        aqiMap.remove(); // Clear old map if reloading
    }

    aqiMap = L.map('aqiMap').setView([6.9271, 79.8612], 12);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors',
        maxZoom: 18,
    }).addTo(aqiMap);

    aqiSensors.forEach(sensor => {
        const marker = L.circleMarker([sensor.latitude, sensor.longitude], {
            radius: 10,
            fillColor: aqiGetColor(sensor.latestAQI),
            color: '#000',
            weight: 1,
            opacity: 1,
            fillOpacity: 0.8
        }).addTo(aqiMap);

        marker.bindPopup(`<strong>${sensor.location}</strong><br/>AQI: ${sensor.latestAQI}`);
        marker.on('click', () => aqiUpdateCharts(sensor.sensorId, sensor.location));
        aqiMarkers[sensor.sensorId] = marker;
    });
}

async function aqiSelectSensor() {
    const select = document.getElementById('sensorSelect');
    const sensorId = select.value;
    const sensor = aqiSensors.find(s => s.sensorId == sensorId);

    if (sensor) {
        aqiMap.setView([sensor.latitude, sensor.longitude], 13);
        aqiMarkers[sensor.sensorId].openPopup();
        await aqiUpdateCharts(sensor.sensorId, sensor.location);
    }
}

async function aqiUpdateCharts(sensorId, locationName) {
    document.getElementById('selected-sensor').textContent = locationName;

    const historyRes = await fetch(`/Dashboards/GetSensorHistory?sensorId=${sensorId}`);
    const history = await historyRes.json();

    const allRes = await fetch('/Dashboards/GetAllCurrentAQI');
    const allAQI = await allRes.json();

    const lineCtx = document.getElementById('aqiLineChart').getContext('2d');
    const pieCtx = document.getElementById('aqiPieChart').getContext('2d');
    const barCtx = document.getElementById('aqiBarChart').getContext('2d');

    if (window.lineChart) window.lineChart.destroy();
    if (window.pieChart) window.pieChart.destroy();
    if (window.barChart) window.barChart.destroy();

    window.lineChart = new Chart(lineCtx, {
        type: 'line',
        data: {
            labels: ['-6h', '-5h', '-4h', '-3h', '-2h', '-1h', 'Now'],
            datasets: [{
                label: 'AQI',
                data: history,
                borderColor: aqiGetColor(history[history.length - 1]),
                backgroundColor: aqiGetColor(history[history.length - 1]),
                fill: false,
                tension: 0.3
            }]
        },
        options: { responsive: true, maintainAspectRatio: false }
    });

    const breakdown = [0, 0, 0, 0];
    history.forEach(aqi => {
        if (aqi <= 50) breakdown[0]++;
        else if (aqi <= 100) breakdown[1]++;
        else if (aqi <= 150) breakdown[2]++;
        else breakdown[3]++;
    });

    window.pieChart = new Chart(pieCtx, {
        type: 'pie',
        data: {
            labels: ['Good', 'Moderate', 'Unhealthy', 'Very Unhealthy'],
            datasets: [{
                data: breakdown,
                backgroundColor: ['green', 'yellow', '#FF8C00', 'red']
            }]
        },
        options: { responsive: true, maintainAspectRatio: false }
    });

    const barColors = allAQI.map(s => aqiGetColor(s.latestAQI));
    window.barChart = new Chart(barCtx, {
        type: 'bar',
        data: {
            labels: allAQI.map(s => s.location),
            datasets: [{
                label: 'Current AQI',
                data: allAQI.map(s => s.latestAQI),
                backgroundColor: barColors
            }]
        },
        options: { responsive: true, maintainAspectRatio: false }
    });
}

// Run when page is loaded
document.addEventListener('DOMContentLoaded', () => {
    fetchSensors();

    // Hook dropdown selection change
    const select = document.getElementById('sensorSelect');
    select.addEventListener('change', aqiSelectSensor);
});
// at bottom of aqi-dashboard.js
fetchSensors();
