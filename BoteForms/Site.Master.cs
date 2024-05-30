using System;
using System.Web;
using System.Web.UI;

namespace BoteForms
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var nombre = HttpContext.Current.User.Identity.Name;
                    aPerfil.InnerText = "Bienvenido " + nombre;
                    liPerfil.Visible = true;
                    liAcceder.Visible = false;

                }                

            }
        }
    }
}
