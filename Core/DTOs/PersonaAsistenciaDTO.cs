using System.ComponentModel.DataAnnotations;

namespace GYM.Core.DTOs
{
    public class PersonaAsistenciaDTO
    {
        public string ci { get; set; } = null!;
        public DateOnly fecha { get; set; }
    }
}
