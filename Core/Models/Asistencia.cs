using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace GYM.Core.Models
{
    [Index(nameof(fecha), IsUnique = true)]
    public class Asistencia
    {
        [Key]
        public int id_asistencia { get; set; }
        public DateOnly fecha { get; set; }
        public bool estado { get; set; } = true;
        public ICollection<PersonaAsistencia>? PersonaAsistencias { get; set; }
    }
}
