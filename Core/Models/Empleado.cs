using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GYM.Core.Models;

namespace Models
{
    public class Empleado
    {
        [Key]
        public int id_empleado { get; set; }

        public DateOnly fecha { get; set; }
        public string pwd { get; set; } = null!;
        public bool estado { get; set; } = true;

        public int id_persona { get; set; }

        [ForeignKey(nameof(id_persona))]
        [JsonIgnore]
        public Persona persona { get; set; } = null!;

        public ICollection<EmpleadoCargo>? empleadocargo { get; set; }
        public ICollection<Ventas> Ventas {get;set;} = new List<Ventas>();
    }
}