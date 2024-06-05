window.addEventListener("beforeunload", function () {
    // Realizar una llamada AJAX para cerrar la sesión del usuario
    // Puedes usar jQuery u otra biblioteca para realizar la llamada AJAX
    // Aquí se asume que tienes una función llamada CerrarSesion en tu página ASP.NET
    // Asegúrate de ajustar la URL de la llamada AJAX según tu configuración
    $.ajax({
        type: "POST",
        url: "Login.aspx/CerrarSesion",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // Éxito: la sesión del usuario se ha cerrado
        },
        error: function (response) {
            // Error: no se pudo cerrar la sesión del usuario
        }
    });
});
