using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GYM.Core.Models
{
    public class DetalleVenta
    {
        [Key]
        public int id_venta { get; set; }
        [Key]
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        [ForeignKey("id_venta")]
        [JsonIgnore]
        public Ventas ventas { get; set; } = null!;
        [ForeignKey("id_producto")]
        [JsonIgnore]
        public Producto producto { get; set; } = null!;
    }
}
