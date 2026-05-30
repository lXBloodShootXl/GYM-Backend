using System.ComponentModel.DataAnnotations;

namespace GYM.Core.Models
{
    public class Inventario
    {
        [Key]
        public int id_inventario { get; set; }

        public string codigo { get; set; } = null!;

        public string nombre { get; set; } = null!;

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}