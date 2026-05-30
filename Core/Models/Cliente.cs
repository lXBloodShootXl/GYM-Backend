using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GYM.Core.Models
{
    public class Cliente
    {
        [Key]
        public int id_cliente { get; set; }
        public int id_persona { get; set; }
        public DateOnly fecha { get; set; }
        public string pwd { get; set; } = null!;
        public bool estado { get; set; } = true;
        [JsonIgnore]
        [ForeignKey("id_persona")]
        public Persona persona { get; set; } = null!;
        public ICollection<Suscripcion>? Suscripciones { get; set; }
        public ICollection<Ventas> Ventas { get; set; } = new List<Ventas>();
    }
}
