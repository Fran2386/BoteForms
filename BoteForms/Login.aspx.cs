using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using BoteForms.Data;
using BoteForms.modelo;
using BoteForms.Util;

namespace BoteForms
{
    public partial class Login : Page
    {
        protected void btnLoginClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == UsernameLogin.Text);

                    if (usuario != null)
                    {
                        // Hashear la contraseña ingresada con el salt almacenado
                        string hashedPassword = PasswordHelper.HashPassword(PasswordLogin.Text, usuario.Salt);

                        // Comparar el hash de la contraseña ingresada con el hash almacenado
                        if (hashedPassword == usuario.Contraseña)
                        {
                            FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);

                            // Verificar si hay datos en la sesión
                            if (Session["Trabajadores"] != null)
                            {
                                List<Trabajador> listaTrabajadores = Session["Trabajadores"] as List<Trabajador>;
                                GuardarListaBBDD guardador = new GuardarListaBBDD();

                                // Aquí obtienes el ID del usuario autenticado
                                var userId = usuario.UsuarioID;

                                // Guardar la lista de trabajadores asociados al usuario autenticado
                                if (guardador.GuardarListaTemporal(listaTrabajadores, Session, userId))
                                {
                                    // Mostrar ventana de confirmación                          
                                    ScriptManager.RegisterStartupScript(this, GetType(), "showMessage", "if(confirm('Datos guardados correctamente.')){ window.location.href = 'Perfil.aspx'; }", true);
                                    return; // Importante: detener la ejecución para evitar redirecciones múltiples
                                }
                                else
                                {
                                    // Manejar el caso en el que no se pueda guardar la lista de trabajadores
                                    // Puedes mostrar un mensaje de error o realizar alguna otra acción
                                }
                            }
                            else
                            {
                                // Si no hay datos en la sesión, simplemente redirige al usuario a "Perfil.aspx"
                                Response.Redirect("~/Perfil.aspx");
                                return; // Importante: detener la ejecución para evitar redirecciones múltiples
                            }
                        }
                        else
                        {
                            LoginErrorMessage.Text = "Nombre de usuario o contraseña incorrectos.";
                        }
                    }
                    else if (string.IsNullOrEmpty(UsernameLogin.Text) && string.IsNullOrEmpty(PasswordLogin.Text))
                    {
                        LoginErrorMessage.Text = "Por favor rellene los datos";
                    }
                    else
                    {
                        LoginErrorMessage.Text = "Nombre de usuario o contraseña incorrectos.";
                    }
                }
            }
        }

        protected void btnRegistroClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Registro.aspx");
        }
  
        protected void CerrarSesion()
        {
            FormsAuthentication.SignOut();
        }
    }
}


