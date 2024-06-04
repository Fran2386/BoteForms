using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Security;
using BoteForms.Data;
using System.Data.Entity.Migrations;

namespace BoteForms
{   
    public partial class Perfil : Page
    {
        _Default Default = new _Default();

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
                    CargarTrabajadores();
                }
                else
                {
                    CargarTrabajadores();
                }
            }
        }

        private void CargarTrabajadores()
        {
            using (var db = new AppDbContext())
            {
                var usuarioActual = IDusuarioActivo();
                if (usuarioActual != 0)
                {

                    var trabajadores = db.Trabajadores.Where(t => t.UsuarioID == usuarioActual).ToList();
                    foreach (var trabajador in trabajadores)
                    {
                        var row = new TableRow();

                        var chkCell = new TableCell();
                        var chkTrabajador = new CheckBox
                        {
                            ID = "chk" + trabajador.Nombre,
                            Text = trabajador.Nombre,
                            CssClass = "checkbox",
                            AutoPostBack = true
                        };
                        chkTrabajador.CheckedChanged += new EventHandler(chkHabilitarTextBox);
                        chkCell.Controls.Add(chkTrabajador);

                        var txtCell = new TableCell();
                        var txtTrabajador = new TextBox
                        {
                            ID = "txt" + trabajador.Nombre,
                            CssClass = "form-control",
                            Enabled = false,
                            TextMode = TextBoxMode.Number,                          
                            AutoPostBack = true
                        };
                        txtTrabajador.Attributes.Add("min", "1");
                        txtTrabajador.Attributes.Add("max", "168");
                        txtTrabajador.Attributes.Add("required", "true");
                        txtTrabajador.Attributes.Add("placeholder", "Introduce las horas");
                        txtCell.Controls.Add(txtTrabajador);

                        var lblCell = new TableCell();
                        var lblTrabajador = new Label
                        {
                            ID = "lbl" + trabajador.Nombre,
                            CssClass = "form-control"
                        };
                        lblCell.Controls.Add(lblTrabajador);

                        row.Cells.Add(chkCell);
                        row.Cells.Add(txtCell);
                        row.Cells.Add(lblCell);

                        phTrabajadores.Controls.Add(row);
                    }
                }
            }
        }

        protected void BtnCalcularClick(object sender, EventArgs e)
        {
            bool algunCheckMarcado = false;

            // Comprobar si hay al menos un CheckBox marcado
            foreach (TableRow row in phTrabajadores.Controls)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (Control innerControl in cell.Controls)
                    {
                        if (innerControl is CheckBox chk && chk.Checked)
                        {
                            algunCheckMarcado = true;
                            break;
                        }
                    }
                    if (algunCheckMarcado) break;
                }
                if (algunCheckMarcado) break;
            }

            // Si no hay ningún CheckBox marcado, salir del método
            if (!algunCheckMarcado)
            {
                // Opción: Mostrar un mensaje al usuario indicando que debe marcar al menos un CheckBox
                lblMensaje.Text = "Debes marcar al menos un trabajador.";
                lblMensaje.Visible = true;
                return;
            }
            lblMensaje.Visible = false;
            Dictionary<string, int> valoresTrabajadores = new Dictionary<string, int>();

            using (var db = new AppDbContext())
            {
                var usuarioActual = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == User.Identity.Name);
                if (usuarioActual != null)
                {
                    var trabajadores = db.Trabajadores.Where(t => t.UsuarioID == usuarioActual.UsuarioID).ToList();

                    foreach (TableRow row in phTrabajadores.Controls)
                    {
                        string nombreTrabajador = null;
                        int horasTrabajadas = 0;

                        foreach (TableCell cell in row.Cells)
                        {
                            foreach (Control innerControl in cell.Controls)
                            {
                                if (innerControl is CheckBox chk && chk.ID.StartsWith("chk"))
                                {
                                    nombreTrabajador = chk.ID.Substring(3);
                                }
                                else if (innerControl is TextBox txt && txt.ID.StartsWith("txt") && nombreTrabajador != null)
                                {
                                    // Comprobamos si el CheckBox correspondiente está marcado
                                    CheckBox chkControl = row.FindControl("chk" + nombreTrabajador) as CheckBox;
                                    if (chkControl != null && chkControl.Checked)
                                    {
                                        // Solo consideramos el valor del TextBox si el CheckBox está marcado
                                        if (int.TryParse(txt.Text, out horasTrabajadas))
                                        {
                                            valoresTrabajadores[nombreTrabajador] = horasTrabajadas;
                                        }
                                        var trabajador = trabajadores.FirstOrDefault(t => t.Nombre == nombreTrabajador);
                                        if (trabajador != null)
                                        {
                                            trabajador.Horas = horasTrabajadas;
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Aquí calculamos el resultado para cada trabajador y lo almacenamos en un diccionario
                    int totalHorasTrabajadores = valoresTrabajadores.Values.Sum();
                    var resultados = new Dictionary<string, double>();

                    if (totalHorasTrabajadores > 0 && int.TryParse(txtBote.Text, out int valorEntero))
                    {
                        double cashXHora = (double)valorEntero / totalHorasTrabajadores;

                        foreach (var trabajador in valoresTrabajadores)
                        {
                            resultados[trabajador.Key] = cashXHora * trabajador.Value;
                        }
                    }
                    // Finalmente, actualizamos los Labels con los resultados calculados
                    foreach (TableRow row in phTrabajadores.Controls)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            foreach (Control innerControl in cell.Controls)
                            {
                                if (innerControl is Label lbl && lbl.ID.StartsWith("lbl"))
                                {
                                    string nombreTrabajador = lbl.ID.Substring(3);
                                    string resultadoConSimbolo = resultados.ContainsKey(nombreTrabajador) ? resultados[nombreTrabajador].ToString("F2") + " €" : string.Empty;
                                    lbl.Text = resultadoConSimbolo;
                                    var trabajador = trabajadores.FirstOrDefault(t => t.Nombre == nombreTrabajador);

                                    var ultimoBote = resultados.ContainsKey(nombreTrabajador) ? (decimal)resultados[nombreTrabajador] : 0;
                                    decimal boteAcumulado = Default.BoteAcumulado(ultimoBote, nombreTrabajador);
                                    trabajador.UltimoBote = ultimoBote;
                                    trabajador.BoteAcumulado = boteAcumulado;
                                    db.Trabajadores.AddOrUpdate(t => new { t.UltimoBote, t.BoteAcumulado });
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void chkHabilitarTextBox(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk != null)
            {
                foreach (Control control in phTrabajadores.Controls)
                {
                    if (control is TableRow row)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            foreach (Control innerControl in cell.Controls)
                            {
                                if (innerControl is TextBox txt && txt.ID == "txt" + chk.Text)
                                {
                                    txt.Enabled = chk.Checked;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }


        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Perfil.aspx");
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }

        protected void RadioButttonSeleccionado(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton == null) return;

            using (var db = new AppDbContext())
            {
                var usuarioActual = IDusuarioActivo();
                if (usuarioActual != 0)
                {
                    var trabajadores = db.Trabajadores.Where(t => t.UsuarioID == usuarioActual).ToList();
                    foreach (Control control in phTrabajadores.Controls)
                    {
                        if (control is TableRow row)
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                foreach (Control innerControl in cell.Controls)
                                {
                                    if (innerControl is TextBox txt)
                                    {
                                        // Obtener el nombre del trabajador asociado al TextBox
                                        string nombreTrabajador = txt.ID.Substring(3); // Suponiendo que el ID del TextBox es "txtNombreTrabajador"

                                        // Buscar el trabajador en la lista
                                        var trabajador = trabajadores.FirstOrDefault(t => t.Nombre == nombreTrabajador);
                                        if (trabajador != null)
                                        {
                                            // Asignar las horas del trabajador al TextBox
                                            txt.Text = trabajador.Horas.ToString();
                                            txt.Attributes.Remove("placeholder");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
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


