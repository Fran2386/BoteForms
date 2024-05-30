using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoteForms.modelo
{
    [Table("Trabajadores")] // Especifica el nombre de la tabla aquí
    public class Trabajador
    {
        [Key]
        public int TrabajadorID { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int Horas { get; set; }

        [Required]
        public decimal UltimoBote { get; set; }

        public decimal BoteAcumulado { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

