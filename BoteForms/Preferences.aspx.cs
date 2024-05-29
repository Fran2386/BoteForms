using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

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
                MensajeBienvenida.Text = $"Bienvenido, {User.Identity.Name}!";
                // Aquí puedes cargar y mostrar las preferencias del usuario
            }
        }
        protected void BtnCalcularClick(object sender, EventArgs e)

        {
            // Obtener el total de horas ingresadas por todos los trabajadores
            int totalHoras = 0;
            foreach (Control control in phTrabajadores.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtHoras = (TextBox)control;
                    int horas;
                    if (int.TryParse(txtHoras.Text, out horas))
                    {
                        totalHoras += horas;
                    }
                }
            }

            // Calcular y mostrar el resultado para cada trabajador
            int importeBote = Convert.ToInt32(txtBote.Text); // Suponiendo que el importe del bote está en el TextBox txtBote
            foreach (Control control in phTrabajadores.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtHoras = (TextBox)control;
                    string[] controlID = txtHoras.ID.Split('_');
                    int index = Convert.ToInt32(controlID[1]);

                    CheckBox chkTrabajador = (CheckBox)phTrabajadores.FindControl("chkTrabajador_" + index);
                    Label lblResultado = (Label)phTrabajadores.FindControl("lblResultado_" + index);

                    if (chkTrabajador != null && chkTrabajador.Checked)
                    {
                        int horasTrabajador;
                        if (int.TryParse(txtHoras.Text, out horasTrabajador))
                        {
                            // Calcular el resultado y mostrarlo en el Label correspondiente al trabajador
                            double resultado = (horasTrabajador / (double)totalHoras) * importeBote;
                            lblResultado.Text = resultado.ToString("C"); // Muestra el resultado como moneda
                        }
                    }
                }
            }
        }





        protected void ddlNumeroTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numTrabajadores = Convert.ToInt32(ddlNumeroTrabajadores.SelectedValue);

            phTrabajadores.Controls.Clear(); // Limpiamos los controles anteriores

            for (int i = 0; i < numTrabajadores; i++)
            {
                CheckBox chkTrabajador = new CheckBox();
                chkTrabajador.ID = "chkTrabajador_" + i;
                chkTrabajador.Text = "Trabajador " + (i + 1);
                chkTrabajador.CssClass = "checkbox";

                TextBox txtHoras = new TextBox();
                txtHoras.ID = "txtHoras_" + i;
                txtHoras.CssClass = "form-control";
                txtHoras.TextMode = TextBoxMode.Number;
                txtHoras.Enabled = true;

                Label lblResultado = new Label();
                lblResultado.ID = "lblResultado_" + i;
                lblResultado.CssClass = "form-control";

                phTrabajadores.Controls.Add(chkTrabajador);
                phTrabajadores.Controls.Add(new LiteralControl("<br />"));
                phTrabajadores.Controls.Add(txtHoras);
                phTrabajadores.Controls.Add(new LiteralControl("<br />"));
                phTrabajadores.Controls.Add(lblResultado);
                phTrabajadores.Controls.Add(new LiteralControl("<hr />"));
            }
        }

    }
}
