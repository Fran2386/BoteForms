using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoteForms.Data;
using BoteForms.modelo;
using BoteForms.Util;

namespace BoteForms
{
    public partial class _Default : Page
    {
        private readonly GuardarListaBBDD guardador;

        public _Default()
        {
            guardador = new GuardarListaBBDD(this);
            // Código de inicialización si es necesario
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
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
            Botones.Visible = true;
            phTrabajadores.Controls.Clear(); // Limpiamos los controles anteriores

            for (int i = 0; i < numTrabajadores; i++)
            {
                TableRow row = new TableRow();

                TableCell cellNumero = new TableCell();
                Label lblNumero = new Label();
                lblNumero.ID = "lblNumero_" + i;
                lblNumero.Text = (i + 1).ToString();
                lblNumero.CssClass = "form-label";
                cellNumero.Controls.Add(lblNumero);

                TableCell cellNombre = new TableCell();
                TextBox txtNombre = new TextBox();
                txtNombre.ID = "txtNombre_" + i;
                txtNombre.CssClass = "form-control";
                txtNombre.Attributes["placeholder"] = "Nombre (Opcional)";
                cellNombre.Controls.Add(txtNombre);

                TableCell cellHoras = new TableCell();
                TextBox txtHoras = new TextBox();
                txtHoras.ID = "txtHoras_" + i;
                txtHoras.CssClass = "form-control";
                txtHoras.TextMode = TextBoxMode.Number;
                txtHoras.Attributes["min"] = "1"; 
                txtHoras.Attributes["max"] = "168";                 
                txtHoras.Attributes["placeholder"] = "Introduce las horas";
                txtHoras.Enabled = true;
                cellHoras.Controls.Add(txtHoras);

                TableCell cellResultado = new TableCell();
                Label lblResultado = new Label();
                lblResultado.ID = "lblResultado_" + i;
                lblResultado.CssClass = "form-control";
                cellResultado.Controls.Add(lblResultado);

                row.Cells.Add(cellNumero);
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
            if (numTrabajadores < 1) 
            {
                Botones.Visible = false;
            }
        }

        protected void BtnCalcularClick(object sender, EventArgs e)
        {
            if (ComprobarVacio()) { 
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
                            if (subControl is TextBox txtHoras && txtHoras.ID.StartsWith("txtHoras_"))
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
                    int horasTrabajador = 0;
                    string trabajadorNombre = string.Empty;
                    Label lblResultado = null;
                    int trabajadorNumero = 0;

                    foreach (TableCell cell in row.Cells)
                    {
                        foreach (Control subControl in cell.Controls)
                        {
                            if (subControl is TextBox txtNombre && txtNombre.ID.StartsWith("txtNombre_"))
                            {
                                string[] controlID = txtNombre.ID.Split('_');
                                if (controlID.Length > 1 && int.TryParse(controlID[1], out trabajadorNumero))
                                {
                                    trabajadorNombre = string.IsNullOrEmpty(txtNombre.Text) ? $"Trabajador {trabajadorNumero + 1}" : txtNombre.Text;
                                }
                            }
                            else if (subControl is TextBox txtHoras && txtHoras.ID.StartsWith("txtHoras_"))
                            {
                                if (int.TryParse(txtHoras.Text, out int horas))
                                {
                                    horasTrabajador = horas;
                                }
                            }
                            else if (subControl is Label label && label.ID != null && label.ID.StartsWith("lblResultado_"))
                            {
                                lblResultado = label;
                            }
                        }
                    }

                    if (lblResultado != null)
                    {
                        // Calcular el resultado y mostrarlo en el Label correspondiente al trabajador
                        double resultado = (horasTrabajador / (double)totalHoras) * importeBote;
                        lblResultado.Text = $"{trabajadorNombre}: {resultado.ToString("C")}";
                    }
                }
            }
        }
     }

        protected void BtnGuardarClick(object sender, EventArgs e)
        {
            if (ComprobarVacio())
            {
                List<Trabajador> listaTrabajadores = new List<Trabajador>();

                foreach (Control control in phTrabajadores.Controls)
                {
                    if (control is TableRow row)
                    {
                        string nombre = string.Empty;
                        int horas = 0;
                        decimal ultimoBote = 0;

                        foreach (TableCell cell in row.Cells)
                        {
                            foreach (Control subControl in cell.Controls)
                            {
                                if (subControl is TextBox txtNombre && txtNombre.ID.StartsWith("txtNombre_"))
                                {
                                    nombre = txtNombre.Text;
                                }
                                else if (subControl is TextBox txtHoras && txtHoras.ID.StartsWith("txtHoras_"))
                                {
                                    int.TryParse(txtHoras.Text, out horas);
                                }
                                else if (subControl is Label lblResultado && lblResultado.ID.StartsWith("lblResultado_"))
                                {
                                    string[] partes = lblResultado.Text.Split(':');
                                    if (partes.Length == 2)
                                    {
                                        // Eliminar el símbolo del euro y cualquier espacio en blanco adicional
                                        string importeSinEuro = partes[1].Trim().Replace("€", "");

                                        // Intentar convertir el importe a un número
                                        if (decimal.TryParse(importeSinEuro, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out decimal resultado))
                                        {
                                            ultimoBote = resultado;
                                        }
                                    }
                                }
                            }
                        }

                        int userId = IDusuarioActivo();

                        var trabajador = new Trabajador
                        {
                            UsuarioID = userId,
                            Nombre = nombre,
                            Horas = horas,
                            UltimoBote = ultimoBote,
                            BoteAcumulado = BoteAcumulado(ultimoBote, nombre),
                        };

                        if (userId == 0)
                        {
                            // Obtener la lista de trabajadores de la sesión o inicializar una nueva si es null
                            List<Trabajador> listaTrabajadoresTemporal = Session["Trabajadores"] as List<Trabajador>;
                            if (listaTrabajadoresTemporal == null)
                            {
                                listaTrabajadoresTemporal = new List<Trabajador>();
                                Session["Trabajadores"] = listaTrabajadoresTemporal;
                            }

                            // Agregar el trabajador a la lista
                            listaTrabajadoresTemporal.Add(trabajador);
                        }
                        else
                        {
                            listaTrabajadores.Add(trabajador);
                        }
                    }
                }

                if (listaTrabajadores.Count > 0)
                {
                    GuardarListaBBDD guardador = new GuardarListaBBDD();
                    if (guardador.GuardarUsuarioActivo(listaTrabajadores))
                    {
                        string user = "TúNombre"; // Aquí deberías obtener el nombre de donde sea que lo tengas almacenado
                        Response.Redirect("Confirmacion.aspx?nombre=" + Server.UrlEncode(user));
                    }
                }
                else
                {
                    // Si no hay usuarios activos, redirige al login
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        public decimal BoteAcumulado(decimal ultimoBote, string nombre)
        {
            using (var db = new AppDbContext())
            {
                var usuarioIdActivo = IDusuarioActivo();
                var trabajador = db.Trabajadores
                                   .Where(t => t.Nombre == nombre && t.UsuarioID == usuarioIdActivo)
                                   .Select(t => new { t.BoteAcumulado, t.UltimoBote })
                                   .FirstOrDefault();

                if (trabajador != null)
                {
                    if (trabajador.BoteAcumulado == 0)
                    {
                        return ultimoBote;
                    }
                    else
                    {
                        return trabajador.BoteAcumulado + ultimoBote;
                    }
                }
                else
                {
                    return ultimoBote;
                }
            }
        }

        public bool ComprobarVacio() 
        {
            if (string.IsNullOrEmpty(txtBote.Text)) 
            {
                lblErrorBote.Text = "Este campo no puede estar vacío";
                return false;
            }
            return true;
        }

        public int IDusuarioActivo()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var db = new AppDbContext())
                {
                    var usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == User.Identity.Name);
                    if (usuario != null)
                    {
                        return usuario.UsuarioID;
                    }
                }
            }
            return 0;
        }
    }
}

