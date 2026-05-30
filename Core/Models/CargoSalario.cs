using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class CargoSalario
    {
        [Key]
        public int Id {get;set;}
        public int Id_Salario {get;set;}
        public int Id_Cargo {get;set;}
        public DateOnly Fecha_Inicio {get;set;}
        public DateOnly Fecha_Fin {get;set;}
        [ForeignKey("Salario_Id")]
        [JsonIgnore]
        public Salario salario {get;set;}
        [ForeignKey("Cargo_Id")]
        [JsonIgnore]
        public Cargo cargo {get;set;}
    }
}