const aqiSensors = [
    { id: 'S1', name: 'Colombo 01', lat: 6.9271, lng: 79.8612, aqi: 42, history: [40, 42, 38, 44, 43, 41, 42] },
    { id: 'S2', name: 'Colombo 05', lat: 6.8911, lng: 79.8775, aqi: 95, history: [90, 92, 91, 93, 94, 95, 95] },
    { id: 'S3', name: 'Colombo 07', lat: 6.9022, lng: 79.8607, aqi: 120, history: [110, 115, 118, 121, 119, 122, 120] },
    { id: 'S4', name: 'Colombo 10', lat: 6.9150, lng: 79.8700, aqi: 160, history: [155, 157, 158, 159, 161, 162, 160] }
];

const aqiSelect = document.getElementById('sensorSelect');
aqiSensors.forEach(sensor => {
    const option = document.createElement('option');
    option.value = sensor.id;
    option.textContent = sensor.name;
    aqiSelect.appendChild(option);
});

function aqiGetColor(aqi) {
    if (aqi <= 50) return 'green';
    if (aqi <= 100) return 'yellow';
    if (aqi <= 150) return '#FF8C00';
    return 'red';
}

const aqiMap = L.map('aqiMap').setView([6.9271, 79.8612], 12);
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; OpenStreetMap contributors'
}).addTo(aqiMap);

const aqiMarkers = {};
aqiSensors.forEach(sensor => {
    const marker = L.circleMarker([sensor.lat, sensor.lng], {
        radius: 10,
        fillColor: aqiGetColor(sensor.aqi),
        color: '#000',
        weight: 1,
        opacity: 1,
        fillOpacity: 0.8
    }).addTo(aqiMap);

    marker.bindPopup(`<strong>${sensor.name}</strong><br/>AQI: ${sensor.aqi}`);
    marker.on('click', () => aqiUpdateCharts(sensor));
    aqiMarkers[sensor.id] = marker;
});

function aqiSelectSensor(id) {
    const sensor = aqiSensors.find(s => s.id === id);
    if (sensor) {
        aqiMap.setView([sensor.lat, sensor.lng], 13);
        aqiMarkers[sensor.id].openPopup();
        aqiUpdateCharts(sensor);
    }
}

function aqiUpdateCharts(sensor) {
    document.getElementById('selected-sensor').textContent = sensor.name;

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
                data: sensor.history,
                borderColor: aqiGetColor(sensor.aqi),
                backgroundColor: aqiGetColor(sensor.aqi),
                fill: false,
                tension: 0.3
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: { beginAtZero: true }
            }
        }
    });

    const breakdown = [0, 0, 0, 0];
    sensor.history.forEach(aqi => {
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
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });

    const barColors = aqiSensors.map(s => aqiGetColor(s.aqi));
    window.barChart = new Chart(barCtx, {
        type: 'bar',
        data: {
            labels: aqiSensors.map(s => s.name),
            datasets: [{
                label: 'Current AQI',
                data: aqiSensors.map(s => s.aqi),
                backgroundColor: barColors
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: { beginAtZero: true }
            }
        }
    });
}
