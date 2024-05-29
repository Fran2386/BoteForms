using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Security;

namespace BoteForms
{
    public partial class Perfil : Page
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
             
            }

        }
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Perfil");
        }

        protected void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectToDatabase();
        }
        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }

        private void ConnectToDatabase()
        {
            // Obtener la cadena de conexión desde el archivo web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CalculadoraBoteDB"].ConnectionString;

            // Intentar abrir una conexión a la base de datos
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    lblMessage.Text = "Conexión exitosa a la base de datos.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al intentar conectar a la base de datos: " + ex.Message;
            }
        }
        protected void chkHabilitarTextBox(object sender, EventArgs e)
        {
            txtAni.Enabled = chkAni.Checked;
            txtCris.Enabled = chkCris.Checked;
            txtDiana.Enabled = chkDiana.Checked;
            txtFran.Enabled = chkFran.Checked;
            txtMarina.Enabled = chkMarina.Checked;
            txtVictor.Enabled = chkVictor.Checked;
            txtYoli.Enabled = chkYoli.Checked;
            txtExtra1.Enabled = chkExtra1.Checked;
            txtExtra2.Enabled = chkExtra2.Checked;
        }

        protected void BtnCalcularClick(object sender, EventArgs e)
        {
            Dictionary<string, int> valoresTrabajadores = new Dictionary<string, int>();

            // Verificar y convertir los valores de los TextBox que tienen CheckBox marcados
            if (chkAni.Checked && int.TryParse(txtAni.Text, out int hrsAni))
            {
                valoresTrabajadores["Ani"] = hrsAni;
            }
            if (chkCris.Checked && int.TryParse(txtCris.Text, out int hrsCris))
            {
                valoresTrabajadores["Cris"] = hrsCris;
            }
            if (chkDiana.Checked && int.TryParse(txtDiana.Text, out int hrsDiana))
            {
                valoresTrabajadores["Diana"] = hrsDiana;
            }
            if (chkFran.Checked && int.TryParse(txtFran.Text, out int hrsFran))
            {
                valoresTrabajadores["Fran"] = hrsFran;
            }
            if (chkMarina.Checked && int.TryParse(txtMarina.Text, out int hrsMarina))
            {
                valoresTrabajadores["Marina"] = hrsMarina;
            }
            if (chkVictor.Checked && int.TryParse(txtVictor.Text, out int hrsVictor))
            {
                valoresTrabajadores["Victor"] = hrsVictor;
            }
            if (chkYoli.Checked && int.TryParse(txtYoli.Text, out int hrsYoli))
            {
                valoresTrabajadores["Yoli"] = hrsYoli;
            }
            if (chkExtra1.Checked && int.TryParse(txtExtra1.Text, out int hrsExtra1))
            {
                valoresTrabajadores["Extra1"] = hrsExtra1;
            }
            if (chkExtra2.Checked && int.TryParse(txtExtra2.Text, out int hrsExtra2))
            {
                valoresTrabajadores["Extra2"] = hrsExtra2;
            }

            // Calcular cashXHora basado en la suma de todas las horas trabajadas
            int totalHorasTrabajadores = valoresTrabajadores.Values.Sum();

            if (totalHorasTrabajadores > 0 && int.TryParse(txtBote.Text, out int valorEntero))
            {
                double cashXHora = (double)valorEntero / totalHorasTrabajadores;

                // Multiplicar cashXHora por cada valor en el diccionario
                Dictionary<string, double> resultados = new Dictionary<string, double>();
                foreach (var trabajador in valoresTrabajadores)
                {
                    resultados[trabajador.Key] = cashXHora * trabajador.Value;
                }

                // Mostrar los resultados en los Labels correspondientes con el símbolo de euro
                foreach (var resultado in resultados)
                {
                    string resultadoConSimbolo = resultado.Value.ToString("F2") + " €";
                    switch (resultado.Key)
                    {
                        case "Ani":
                            lblAni.Text = resultadoConSimbolo;
                            break;
                        case "Cris":
                            lblCris.Text = resultadoConSimbolo;
                            break;
                        case "Diana":
                            lblDiana.Text = resultadoConSimbolo;
                            break;
                        case "Fran":
                            lblFran.Text = resultadoConSimbolo;
                            break;
                        case "Marina":
                            lblMarina.Text = resultadoConSimbolo;
                            break;
                        case "Victor":
                            lblVictor.Text = resultadoConSimbolo;
                            break;
                        case "Yoli":
                            lblYoli.Text = resultadoConSimbolo;
                            break;
                        case "Extra1":
                            lblExtra1.Text = resultadoConSimbolo;
                            break;
                        case "Extra2":
                            lblExtra2.Text = resultadoConSimbolo;
                            break;
                    }
                }
            }
            else
            {
                // Manejar el caso en el que txtBote no contenga un número entero válido o totalHorasTrabajadores sea 0
                if (txtBote.Text == "")
                {
                    txtBote.Attributes["placeholder"] = "Por favor introduce el monto del bote";
                }
                else
                {
                    foreach (var textBox in new List<TextBox> { txtAni, txtCris, txtDiana, txtFran, txtMarina, txtVictor, txtYoli, txtExtra1, txtExtra2 })
                    {
                        if (string.IsNullOrEmpty(textBox.Text))
                        {
                            textBox.Attributes["placeholder"] = "Introduce las horas";
                        }
                    }
                }
            }
        }









        protected void RadioButttonSeleccionado(object sender, EventArgs e)
        {
            // Cast sender to RadioButton
            RadioButton selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton == null) return;

            // Switch based on the RadioButton ID
            switch (selectedRadioButton.ID)
            {
                case "hrsPredefinidas":
                    SetHorasPredefinidas();
                    break;

                case "hrsEditables":
                    SetHorasEditables();
                    break;
            }
        }

        private void SetHorasPredefinidas()
        {
            txtAni.Text = "50";
            txtAni.Attributes.Remove("placeholder");
            txtCris.Text = "50";
            txtCris.Attributes.Remove("placeholder");
            txtDiana.Text = "40";
            txtDiana.Attributes.Remove("placeholder");
            txtFran.Text = "50";
            txtFran.Attributes.Remove("placeholder");
            txtMarina.Text = "";
            txtMarina.Attributes.Remove("placeholder");
            txtVictor.Text = "50";
            txtVictor.Attributes.Remove("placeholder");
            txtYoli.Text = "30";
            txtYoli.Attributes.Remove("placeholder");
            txtExtra1.Text = "";
            txtExtra1.Attributes.Remove("placeholder");
            txtExtra2.Text = "";
            txtExtra2.Attributes.Remove("placeholder");
        }

        private void SetHorasEditables()
        {
            txtAni.Text = "";
            txtAni.Attributes["placeholder"] = "Introduce las horas a computar";
            txtCris.Text = "";
            txtCris.Attributes["placeholder"] = "Introduce las horas a computar";
            txtDiana.Text = "";
            txtDiana.Attributes["placeholder"] = "Introduce las horas a computar";
            txtFran.Text = "";
            txtFran.Attributes["placeholder"] = "Introduce las horas a computar";
            txtMarina.Text = "";
            txtMarina.Attributes["placeholder"] = "Introduce las horas a computar";
            txtVictor.Text = "";
            txtVictor.Attributes["placeholder"] = "Introduce las horas a computar";
            txtYoli.Text = "";
            txtYoli.Attributes["placeholder"] = "Introduce las horas a computar";
            txtExtra1.Text = "";
            txtExtra1.Attributes["placeholder"] = "Introduce las horas a computar";
            txtExtra2.Text = "";
            txtExtra2.Attributes["placeholder"] = "Introduce las horas a computar";
        }

        private void CalculoBote(List<int> valoresEnteros)
        {
            // Inicializa la variable totalHorasTrabajadores
            int totalHorasTrabajadores = 0;

            // Suma todos los valores de la lista
            foreach (int valor in valoresEnteros)
            {
                totalHorasTrabajadores += valor;
            }

            // Ahora totalHorasTrabajadores contiene la suma de todos los valores en la lista

            if (txtBote == null || string.IsNullOrEmpty(txtBote.Text))
            {
                txtBote.Attributes["placeholder"] = "Por favor introduce el monto del bote";
            }
            else
            {
                int valorEntero;
                if (!int.TryParse(txtBote.Text, out valorEntero))
                {
                    txtBote.Attributes["placeholder"] = "El valor ingresado no es válido";
                    // También puedes mostrar un mensaje de error adicional, o realizar otra acción según sea necesario
                }
                else
                {
                    double cashXHora = (double)valorEntero / totalHorasTrabajadores;

                    double[] resultados = new double[valoresEnteros.Count];

                    for (int i = 0; i < valoresEnteros.Count; i++)
                    {
                        resultados[i] = cashXHora * valoresEnteros[i];
                    }

                    // Aquí puedes usar los valores en el arreglo resultados como necesites



                }
            }



        }

    }
}
