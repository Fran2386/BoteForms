using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoteForms
{
    public class Trabajador
    {
        public int TrabajadorId { get; set; }
        public int UsuarioId { get; set; } 
        public string Nombre { get; set; }
        public int Horas { get; set; }
    }

}