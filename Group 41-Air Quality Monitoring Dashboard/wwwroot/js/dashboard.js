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

document.addEventListener('DOMContentLoaded', () => {
    fetch('/simulation/status')
        .then(response => response.json())
        .then(data => {
            if (data.isRunning) {
                document.getElementById('sim-status').innerHTML = 'Simulation Status: <strong>Running</strong>';
                document.getElementById('frequency').value = data.frequency;
            } else {
                document.getElementById('sim-status').innerHTML = 'Simulation Status: <strong>Stopped</strong>';
            }
        });
});

function startSimulation() {
    const frequencyInput = document.getElementById('frequency').value;
    const frequency = parseInt(frequencyInput);

    if (!frequencyInput) {
        alert('Please enter a frequency value!');
        return;
    }

    if (isNaN(frequency) || frequency < 5 || frequency > 15) {
        alert('Frequency must be between 5 and 15 minutes.');
        return;
    }

    fetch('/simulation/start', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ frequency: frequency })
    })
        .then(response => response.json())
        .then(data => {
            document.getElementById('sim-status').innerHTML = 'Simulation Status: <strong>Running</strong>';
        });
}

function stopSimulation() {
    fetch('/simulation/stop', {
        method: 'POST'
    })
        .then(response => response.json())
        .then(data => {
            document.getElementById('sim-status').innerHTML = 'Simulation Status: <strong>Stopped</strong>';
        });
}

function saveThresholds() {
    const moderate = document.getElementById('moderate').value;
    const unhealthy = document.getElementById('unhealthy').value;
    const veryUnhealthy = document.getElementById('veryUnhealthy').value;
    alert(`Thresholds saved:\nModerate Max: ${moderate}\nUnhealthy Min: ${unhealthy}\nVery Unhealthy: ${veryUnhealthy}`);
}

// Load sensors from DB on page load
document.addEventListener("DOMContentLoaded", loadSensors);