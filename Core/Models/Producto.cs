using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Core.Models
{
    public class Producto
    {
        [Key]
        public int id_producto { get; set; }

        public string codigo { get; set; } = null!;

        public string nombre { get; set; } = null!;

        public string descripcion { get; set; } = null!;

        public decimal precio { get; set; }

        public int id_categoria { get; set; }

        [ForeignKey("id_categoria")]
        public Categoria Categoria { get; set; } = null!;

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}