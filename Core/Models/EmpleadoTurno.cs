using Microsoft.EntityFrameworkCore;
using Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GYM.Core.Models
{
    [PrimaryKey(nameof(id_empleado), nameof(id_turno))]
    public class EmpleadoTurno
    {
        public int id_empleado { get; set; }

        public int id_turno { get; set; }

        [ForeignKey("id_empleado")]
        [JsonIgnore]
        public Empleado Empleado { get; set; } = null!;

        [ForeignKey("id_turno")]
        [JsonIgnore]
        public Turno Turno { get; set; } = null!;
    }
}