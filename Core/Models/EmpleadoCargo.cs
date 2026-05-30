using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class EmpleadoCargo
    {
        [Key]
        public int Id {get;set;}
        public int Id_Cargo {get;set;}
        public int Id_Empleado {get;set;}
        public bool Estado {get;set;}=true; 
        public DateOnly FechaIncio {get;set;}
        public DateOnly FechaFin {get;set;}
        [ForeignKey("Cargo_Id")]
        [JsonIgnore]
        public Cargo cargo {get;set;}
        [ForeignKey("id_Empleado")]
        [JsonIgnore]
        public Empleado empleado { get; set; } = null!;
        
    }
}