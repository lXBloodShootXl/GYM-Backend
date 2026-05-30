using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GYM.Core.Models
{
    [Index(nameof(telf), IsUnique = true)]
    public class Telefono
    {
        [Key]
        public int id_telefono { get; set; }
        public string telf { get; set; } = null!;
        public bool estado { get; set; } = true;
        public ICollection<PersonaTelefono>? PersonaTelefonos { get; set; }
    }
}
