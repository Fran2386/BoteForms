using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace BoteForms
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }
    }
}
