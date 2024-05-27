using System;
using System.Web;
using System.Web.UI;

namespace BoteForms
{
    public partial class Preferences : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                WelcomeMessage.Text = $"Welcome, {User.Identity.Name}!";
                // Aquí puedes cargar y mostrar las preferencias del usuario
            }
        }
    }
}
