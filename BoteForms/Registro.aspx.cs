using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using BoteForms.Data;
using BoteForms.modelo;

namespace BoteForms
{
    public partial class Registro : Page
    {
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Validar campos vacíos o con espacios en blanco
                if (string.IsNullOrWhiteSpace(UsernameRegister.Text) ||
                    string.IsNullOrWhiteSpace(PasswordRegister.Text) ||
                    string.IsNullOrWhiteSpace(EmailRegister.Text))
                {
                    RegisterMessage.Text = "Todos los campos son obligatorios y no pueden estar vacíos.";
                    return;
                }

                // Validar el formato del correo electrónico
                if (!ComprobarEmail(EmailRegister.Text))
                {
                    RegisterMessage.Text = "El formato del correo electrónico es incorrecto.";
                    return;
                }

                using (var db = new AppDbContext())
                {
                    var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == UsernameRegister.Text);
                    if (usuarioExistente == null)
                    {
                        string salt = PasswordHelper.GenerateSalt();
                        string hashedPassword = PasswordHelper.HashPassword(PasswordRegister.Text, salt);

                        var usuario = new Usuario
                        {
                            NombreUsuario = UsernameRegister.Text,
                            Contraseña = hashedPassword,
                            Salt = salt, // Asegúrate de tener una columna Salt en tu base de datos
                            Email = EmailRegister.Text,
                            FechaCreacion = DateTime.Now
                        };

                        db.Usuarios.Add(usuario);
                        db.SaveChanges();

                        RegisterMessage.Text = "Registro exitoso. Ahora puede iniciar sesión.";
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        RegisterMessage.Text = "El nombre de usuario ya existe. Elija otro.";
                    }
                }
            }
        }

        private bool ComprobarEmail(string email)
        {
            // Expresión regular para validar el formato del correo electrónico
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}

