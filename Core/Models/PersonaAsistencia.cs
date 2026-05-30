using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GYM.Core.Models
{
    [Index(nameof(id_persona), nameof(id_asistencia), IsUnique = true)]
    public class PersonaAsistencia
    {
        [Key]
        public int id_persona { get; set; }
        [Key]
        public int id_asistencia { get; set; }
        public bool estado { get; set; } = true;
        [ForeignKey("id_persona")]
        [JsonIgnore]
        public Persona persona { get; set; } = null!;
        [ForeignKey("id_asistencia")]
        [JsonIgnore]
        public Asistencia asistencia { get; set; } = null!;
    }
}
