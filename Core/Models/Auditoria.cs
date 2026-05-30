using System.ComponentModel.DataAnnotations;

namespace GYM.Core.Models
{
    public class Auditoria
    {
        [Key]
        public int id_auditoria { get; set; }

        public string tabla { get; set; } = null!;

        public int id_registro { get; set; }

        public string accion { get; set; } = null!;

        public string datos_anteriores { get; set; } = null!;

        public string datos_nuevos { get; set; } = null!;
    }
}