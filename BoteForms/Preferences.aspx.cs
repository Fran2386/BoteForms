using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                if (!IsPostBack)
                {
                    // Inicialización que sólo debe ocurrir una vez
                    InicializarDropDownList();
                }
                else
                {
                    // Reconstruir controles dinámicos en cada PostBack
                    int numTrabajadores = Convert.ToInt32(ddlNumeroTrabajadores.SelectedValue);
                    CrearControlesTrabajadores(numTrabajadores);
                }
            }
        }

        private void InicializarDropDownList()
        {
            ddlNumeroTrabajadores.Items.Clear();
            ddlNumeroTrabajadores.Items.Add(new ListItem("Seleccionar", "0"));
            for (int i = 1; i <= 100; i++)
            {
                ddlNumeroTrabajadores.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void CrearControlesTrabajadores(int numTrabajadores)
        {
            phTrabajadores.Controls.Clear(); // Limpiamos los controles anteriores

            for (int i = 0; i < numTrabajadores; i++)
            {
                TableRow row = new TableRow();

                TableCell cellNombre = new TableCell();
                Label lblTrabajador = new Label();
                lblTrabajador.ID = "lblTrabajador_" + i;
                lblTrabajador.Text = "Trabajador " + (i + 1);
                lblTrabajador.CssClass = "form-label";
                cellNombre.Controls.Add(lblTrabajador);

                TableCell cellHoras = new TableCell();
                TextBox txtHoras = new TextBox();
                txtHoras.ID = "txtHoras_" + i;
                txtHoras.CssClass = "form-control";
                txtHoras.TextMode = TextBoxMode.Number;
                txtHoras.Enabled = true;
                cellHoras.Controls.Add(txtHoras);

                TableCell cellResultado = new TableCell();
                Label lblResultado = new Label();
                lblResultado.ID = "lblResultado_" + i;
                lblResultado.CssClass = "form-control";
                cellResultado.Controls.Add(lblResultado);

                row.Cells.Add(cellNombre);
                row.Cells.Add(cellHoras);
                row.Cells.Add(cellResultado);

                phTrabajadores.Controls.Add(row);
            }
        }

        protected void ddlNumeroTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numTrabajadores = Convert.ToInt32(ddlNumeroTrabajadores.SelectedValue);
            CrearControlesTrabajadores(numTrabajadores);
        }

        protected void BtnCalcularClick(object sender, EventArgs e)
        {
            // Obtener el total de horas ingresadas por todos los trabajadores
            int totalHoras = 0;
            foreach (Control control in phTrabajadores.Controls)
            {
                if (control is TableRow row)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        foreach (Control subControl in cell.Controls)
                        {
                            if (subControl is TextBox txtHoras)
                            {
                                if (int.TryParse(txtHoras.Text, out int horas))
                                {
                                    totalHoras += horas;
                                }
                            }
                        }
                    }
                }
            }

            if (totalHoras == 0)
            {
                // Manejar el caso donde totalHoras es 0 para evitar división por cero
                // Mostrar un mensaje de error o manejarlo de alguna manera
                return;
            }

            // Suponiendo que el importe del bote está en el TextBox txtBote
            if (!int.TryParse(txtBote.Text, out int importeBote))
            {
                // Manejar el caso donde el valor del bote no es un número válido
                return;
            }

            // Calcular y mostrar el resultado para cada trabajador
            foreach (Control control in phTrabajadores.Controls)
            {
                if (control is TableRow row)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        foreach (Control subControl in cell.Controls)
                        {
                            if (subControl is TextBox txtHoras)
                            {
                                string[] controlID = txtHoras.ID.Split('_');
                                if (controlID.Length > 1 && int.TryParse(controlID[1], out int index))
                                {
                                    Label lblResultado = (Label)row.FindControl("lblResultado_" + index);

                                    if (lblResultado != null)
                                    {
                                        if (int.TryParse(txtHoras.Text, out int horasTrabajador))
                                        {
                                            // Calcular el resultado y mostrarlo en el Label correspondiente al trabajador
                                            double resultado = (horasTrabajador / (double)totalHoras) * importeBote;
                                            lblResultado.Text = resultado.ToString("C"); // Muestra el resultado como moneda
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
