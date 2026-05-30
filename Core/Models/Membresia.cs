using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GYM.Core.Models
{
    public class Membresia
    {
        [Key]
        public int id_membresia { get; set; }
        public string codigo { get; set; } = null!;
        public string nombre { get; set; } = null!;
        public int duracion { get; set; }
        public decimal precio { get; set; }
        public bool estado { get; set; } = true;

        public ICollection<Suscripcion>? Suscripciones { get; set; }
    }
}
