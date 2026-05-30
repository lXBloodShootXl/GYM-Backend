using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GYM.Core.Models;

namespace Models
{
    public class Ventas
    {
        [Key]
        public int id_venta { get; set; }

        public string codigo { get; set; } = null!;

        public DateTime fecha { get; set; }

        public int id_empleado { get; set; }

        [ForeignKey("id_empleado")]
        public Empleado Empleado { get; set; } = null!;

        public int id_cliente { get; set; }

        [ForeignKey("id_cliente")]
        public Cliente Cliente { get; set; } = null!;

        public ICollection<DetalleVenta> DetallesVenta { get; set; }
            = new List<DetalleVenta>();
    }
}