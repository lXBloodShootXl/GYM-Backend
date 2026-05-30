using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Core.Models
{
    public class Stock
    {
        public int id_inventario { get; set; }

        public int id_producto { get; set; }

        public int cantidad { get; set; }

        [ForeignKey("id_inventario")]
        public Inventario inventario { get; set; } = null!;

        [ForeignKey("id_producto")]
        public Producto producto { get; set; } = null!;
    }
}