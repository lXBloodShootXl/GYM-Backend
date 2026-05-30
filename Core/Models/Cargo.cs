using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Cargo
    {
        [Key]
        public int Cargo_Id {get;set;}
        public string Codigo {get;set;}
        public string Nombre {get;set;}
        public bool estado { get; set; } = true;
        public ICollection<CargoSalario> CargoSalarioo {get;set;}
    }
}