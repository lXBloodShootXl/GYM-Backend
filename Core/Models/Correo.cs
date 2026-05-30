using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Core.Models
{
    [Index(nameof(correo), IsUnique = true)]
    public class Correo
    {
        [Key]
        public int id_correo { get; set; }
        public string correo { get; set; } = null!;
        public bool estado { get; set; } = true;
        public ICollection<PersonaCorreo>? PersonaCorreos { get; set; }
    }
}
