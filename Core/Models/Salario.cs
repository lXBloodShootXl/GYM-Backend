using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Salario
    {   [Key]
        public int Salario_Id {get;set;}
        public string Codigo {get;set;}
        public int Salarioo {get;set;}
        public bool estado { get; set; } = true;
        public ICollection<CargoSalario> CargoSalario {get;set;}
    }
}   