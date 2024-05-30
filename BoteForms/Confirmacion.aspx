<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Confirmación</title>
    <link rel="stylesheet" type="text/css" href="Content/popup.css">
    <script>
        function cerrarVentana() {
            window.close();
            // Para navegadores que bloquean el cierre de ventanas abiertas por scripts
            if (!window.closed) {
                window.history.back();
            }
        }

        function obtenerNombre() {
            var parametros = window.location.search.substring(1).split('&');
            for (var i = 0; i < parametros.length; i++) {
                var param = parametros[i].split('=');
                if (param[0] === 'nombre') {
                    return decodeURIComponent(param[1]);
                }
            }
            return null;
        }

        function mostrarMensaje() {
            var nombre = obtenerNombre();
            var mensaje = document.getElementById('mensaje');
            if (nombre) {
                mensaje.innerHTML = "Datos guardados correctamente, " + nombre + ".";
            } else {
                mensaje.innerHTML = "Datos guardados correctamente.";
            }
        }

        window.onload = function () {
            mostrarMensaje();
            var confirmacion = document.getElementById('confirmacion');
            confirmacion.style.display = 'block';
        };
    </script>
</head>
<body>
    <div id="confirmacion" class="popup">
        <div class="popup-content">
            <span class="cerrar" onclick="cerrarVentana()">&times;</span>
            <h1>Confirmación</h1>
            <div id="mensaje"></div>
            <button onclick="cerrarVentana()">Aceptar</button>
        </div>
    </div>
</body>
</html>

