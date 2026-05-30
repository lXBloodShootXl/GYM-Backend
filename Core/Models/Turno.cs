using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GYM.Core.Models
{
    [Index(nameof(codigo), IsUnique = true)]
    public class Turno
    {
        [Key]
        public int id_turno { get; set; }
        public string codigo { get; set; } = null!;
        public string nombre { get; set; } = null!;
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public bool estado { get; set; } = true;
    }
}
