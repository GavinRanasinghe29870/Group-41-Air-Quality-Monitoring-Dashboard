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

// Load sensors from DB on page load
document.addEventListener("DOMContentLoaded", loadSensors);