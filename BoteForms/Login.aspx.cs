using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using BoteForms.Data;

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
                    var usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == UsernameLogin.Text && u.Contraseña == PasswordLogin.Text); // Aquí deberías comparar la contraseña con hashing
                    if (usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);
                        Response.Redirect("~/Perfil.aspx");
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

    }
}
