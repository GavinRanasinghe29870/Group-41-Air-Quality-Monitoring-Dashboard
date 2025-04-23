const sensorList = document.getElementById('sensor-list');
const sensorForm = document.getElementById('sensor-form');
const activeCount = document.getElementById('active-count');
const simStatus = document.getElementById('simulation-running');
const navLinks = document.querySelectorAll('.nav-link');
const sections = document.querySelectorAll('.main-content');

function showSection(id, event) {
    sections.forEach(section => section.classList.remove('active'));
    document.getElementById(id).classList.add('active');
    navLinks.forEach(link => link.classList.remove('active'));
    event.target.closest('a').classList.add('active');
}

let sensors = [];

if (sensorForm) {
    sensorForm.addEventListener('submit', function (e) {
        e.preventDefault();
        const sensor = {
            id: document.getElementById('sensorId').value,
            location: document.getElementById('location').value,
            lat: parseFloat(document.getElementById('lat').value),
            lng: parseFloat(document.getElementById('lng').value)
        };
        sensors.push(sensor);
        renderSensors();
        sensorForm.reset();
    });
}

function renderSensors() {
    sensorList.innerHTML = '';
    sensors.forEach((sensor, index) => {
        const item = document.createElement('li');
        item.className = 'list-group-item d-flex justify-content-between align-items-center';
        item.innerHTML = `${sensor.id} - ${sensor.location} <button class="btn btn-sm btn-outline-danger" onclick="removeSensor(${index})">Remove</button>`;
        sensorList.appendChild(item);
    });
    activeCount.textContent = sensors.length;
}

function removeSensor(index) {
    sensors.splice(index, 1);
    renderSensors();
}

function startSimulation() {
    document.getElementById('sim-status').innerHTML = 'Simulation Status: <strong>Running</strong>';
    simStatus.textContent = 'Yes';
}

function stopSimulation() {
    document.getElementById('sim-status').innerHTML = 'Simulation Status: <strong>Stopped</strong>';
    simStatus.textContent = 'No';
}

function saveThresholds() {
    const moderate = document.getElementById('moderate').value;
    const unhealthy = document.getElementById('unhealthy').value;
    const veryUnhealthy = document.getElementById('veryUnhealthy').value;
    alert(`Thresholds saved:\nModerate Max: ${moderate}\nUnhealthy Min: ${unhealthy}\nVery Unhealthy: ${veryUnhealthy}`);
}
