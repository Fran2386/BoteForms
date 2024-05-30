using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                using (var db = new AppDbContext())
                {
                    var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == UsernameRegister.Text);
                    if (usuarioExistente == null)
                    {
                        var usuario = new Usuario
                        {
                            NombreUsuario = UsernameRegister.Text,
                            Contraseña = PasswordRegister.Text, // Aquí deberías usar hashing
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
    }
}