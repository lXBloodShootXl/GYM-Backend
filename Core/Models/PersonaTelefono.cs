using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GYM.Core.Models
{
    [Index(nameof(ci), nameof(telf), nameof(fecha_inicio), IsUnique = true)]
    public class PersonaTelefono
    {
        [Key]
        public int id_persona { get; set; }
        [Key]
        public int id_telefono { get; set; }
        public string ci { get; set; } = null!;
        public string telf { get; set; } = null!;
        public DateOnly fecha_inicio { get; set; }
        public DateOnly? fecha_fin { get; set; }
        public bool estado { get; set; } = true;
        [ForeignKey("id_persona")]
        [JsonIgnore]
        public Persona persona { get; set; } = null!;
        [ForeignKey("id_telefono")]
        [JsonIgnore]
        public Telefono Telefono { get; set; } = null!;
    }
}
