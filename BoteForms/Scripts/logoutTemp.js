var isLoggedIn = true; // Asegúrate de que esta variable refleje el estado real de inicio de sesión del usuario

var timeoutInMinutes = 10;
var timeoutWarningInMinutes = 9.5;

var sessionTimeout;
var warningTimeout;

function startSessionTimer() {
    if (isLoggedIn) {
        // Inicia el temporizador de sesión para cerrar sesión después de 10 minutos de inactividad
        sessionTimeout = setTimeout(logout, timeoutInMinutes * 60 * 1000);
        // Inicia el temporizador de advertencia para mostrar una advertencia 30 segundos antes de la expiración
        warningTimeout = setTimeout(showTimeoutWarning, timeoutWarningInMinutes * 60 * 1000);
    }
}

function resetSessionTimer() {
    if (isLoggedIn) {
        // Reinicia el temporizador de sesión y el temporizador de advertencia
        clearTimeout(sessionTimeout);
        clearTimeout(warningTimeout);
        startSessionTimer();
    }
}

function logout() {
    // Función para cerrar sesión
    window.location = 'Logout.aspx'; // Reemplaza 'Logout.aspx' con la página que maneja el cierre de sesión
}

function showTimeoutWarning() {
    // Muestra una advertencia de cierre de sesión
    alert('Tu sesión expirará en 30 segundos debido a inactividad.');
}

window.onload = function () {
    if (isLoggedIn) {
        startSessionTimer();
    }
};

document.onmousemove = function () {
    if (isLoggedIn) {
        resetSessionTimer();
    }
};

document.onkeypress = function () {
    if (isLoggedIn) {
        resetSessionTimer();
    }
};

window.addEventListener("beforeunload", function (event) {
    if (isLoggedIn) {
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "Logout.aspx", false); // 'false' hace la llamada sincrónica
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        xhr.send(JSON.stringify({}));
    }
});

