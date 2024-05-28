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
            // Crear un diccionario para almacenar los nombres de los trabajadores y sus horas trabajadas
            Dictionary<string, int> valoresTrabajadores = new Dictionary<string, int>();

            // Recorrer todos los controles CheckBox y TextBox generados dinámicamente
            foreach (Control control in phTrabajadores.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox chkTrabajador = (CheckBox)control;
                    if (chkTrabajador.Checked)
                    {
                        string nombreTrabajador = chkTrabajador.Text;
                        string idTextBox = "txtHoras_" + chkTrabajador.ID.Substring(chkTrabajador.ID.IndexOf("_") + 1);
                        TextBox txtHoras = (TextBox)phTrabajadores.FindControl(idTextBox);
                        if (txtHoras != null && !string.IsNullOrEmpty(txtHoras.Text) && int.TryParse(txtHoras.Text, out int horas))
                        {
                            valoresTrabajadores[nombreTrabajador] = horas;
                        }
                    }
                }
            }

            // Calcular el total de horas trabajadas por todos los trabajadores
            int totalHorasTrabajadores = valoresTrabajadores.Values.Sum();

            // Verificar si el total de horas es válido y calcular el bote si es así
            if (totalHorasTrabajadores > 0 && int.TryParse(txtBote.Text, out int valorEntero))
            {
                double cashXHora = (double)valorEntero / totalHorasTrabajadores;

                // Calcular el pago para cada trabajador y mostrar los resultados en los labels correspondientes
                foreach (var trabajador in valoresTrabajadores)
                {
                    string nombreTrabajador = trabajador.Key;
                    int horasTrabajadas = trabajador.Value;
                    double pagoTrabajador = cashXHora * horasTrabajadas;

                    Label lblResultado = (Label)phTrabajadores.FindControl("lblResultado_" + nombreTrabajador.Substring(nombreTrabajador.IndexOf(" ") + 1));
                    if (lblResultado != null)
                    {
                        lblResultado.Text = pagoTrabajador.ToString("F2") + " €";
                    }
                }
            }
            else
            {
                // Manejar el caso en el que el total de horas o el valor del bote no sean válidos
                if (txtBote.Text == "")
                {
                    txtBote.Attributes["placeholder"] = "Por favor introduce el monto del bote";
                }
                else
                {
                    foreach (Control control in phTrabajadores.Controls)
                    {
                        if (control is TextBox)
                        {
                            TextBox textBox = (TextBox)control;
                            if (string.IsNullOrEmpty(textBox.Text))
                            {
                                textBox.Attributes["placeholder"] = "Introduce las horas";
                            }
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
                txtHoras.Enabled = false;

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
