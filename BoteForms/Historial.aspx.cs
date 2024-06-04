using BoteForms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BoteForms
{
    public partial class Historial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTrabajadores();
        }

        private void CargarTrabajadores()
        {
            using (var db = new AppDbContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == User.Identity.Name);
                if (usuario != null)
                {

                    var trabajadores = db.Trabajadores.Where(t => t.UsuarioID == usuario.UsuarioID).ToList();
                    foreach (var trabajador in trabajadores)
                    {
                        var row = new TableRow();

                        var lbl1 = new TableCell();
                        var lblTrabajador = new Label
                        {
                            ID = "lbl" + trabajador.Nombre,
                            CssClass = "form-control",
                            Text = trabajador.Nombre
                        };
                        lbl1.Controls.Add(lblTrabajador);

                        var lbl2 = new TableCell();
                        var lblUltimoBote = new Label
                        {
                            ID = "lbl" + trabajador.UltimoBote,
                            CssClass = "form-control",
                            Text = (trabajador.UltimoBote).ToString("F2") + " €"
                    };
                        lbl2.Controls.Add(lblUltimoBote);

                        var lbl3 = new TableCell();
                        var lblBoteAcumulado = new Label
                        {
                            ID = "lbl" + trabajador.BoteAcumulado,
                            CssClass = "form-control",
                            Text = (trabajador.BoteAcumulado).ToString("F2") + " €"
                        };
                        lbl3.Controls.Add(lblBoteAcumulado);


                        row.Cells.Add(lbl1);
                        row.Cells.Add(lbl2);
                        row.Cells.Add(lbl3);

                        phTrabajadores.Controls.Add(row);
                    }
                }
            }
        }
        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Perfil.aspx");
        }
    }
}