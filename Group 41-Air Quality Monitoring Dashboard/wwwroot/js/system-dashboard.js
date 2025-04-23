function sysadminShowSection(id, event) {
    document.querySelectorAll('.section').forEach(section => section.classList.remove('active'));
    document.getElementById(id).classList.add('active');

    document.querySelectorAll('.sidebar a').forEach(link => link.classList.remove('active'));
    event.target.closest('a').classList.add('active');
}
