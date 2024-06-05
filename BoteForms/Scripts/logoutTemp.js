var timeoutInMinutes = 10;
var timeoutWarningInMinutes = 9.5;

var timeoutWarning;

function startSessionTimer() {
    // Inicia el temporizador de sesión
    setTimeout(logout, timeoutInMinutes * 60 * 1000);
    // Muestra una advertencia de cierre de sesión en 30 segundos antes de la expiración
    timeoutWarning = setTimeout(showTimeoutWarning, (timeoutInMinutes - timeoutWarningInMinutes) * 60 * 1000);
}

function resetSessionTimer() {
    // Reinicia el temporizador de sesión
    clearTimeout(timeoutWarning);
    startSessionTimer();
}

function logout() {
    // Función para cerrar sesión
    window.location = 'Logout.aspx'; // Reemplaza 'Logout.aspx' con la página que maneja el cierre de sesión
}

function showTimeoutWarning() {
    // Muestra una advertencia de cierre de sesión
    alert('Tu sesión expirará en 30 segundos debido a inactividad.');
}

// Iniciar el temporizador de sesión cuando se carga la página
window.onload = startSessionTimer;

// Reiniciar el temporizador de sesión cuando se detecta actividad del usuario
document.onmousemove = resetSessionTimer;
document.onkeypress = resetSessionTimer;


