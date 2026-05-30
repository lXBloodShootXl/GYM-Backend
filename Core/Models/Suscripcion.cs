using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GYM.Core.Models
{
    public class Suscripcion
    {
        public int id_cliente { get; set; }
        public int id_membresia { get; set; }
        public DateOnly fecha_inicio { get; set; }
        public DateOnly fecha_fin { get; set; }
        public bool estado { get; set; } = true;
        [ForeignKey("id_cliente")]
        [JsonIgnore]
        public Cliente cliente{ get; set; } = null!;
        [ForeignKey("id_membresia")]
        [JsonIgnore]
        public Membresia membresia { get; set; } = null!;
    }
}
