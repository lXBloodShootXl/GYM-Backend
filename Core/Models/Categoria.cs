using System.ComponentModel.DataAnnotations;

namespace GYM.Core.Models
{
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set; }

        public string codigo { get; set; } = null!;

        public string nombre { get; set; } = null!;

        public string descripcion { get; set; } = null!;

        public ICollection<Producto> Productos { get; set; }
            = new List<Producto>();
    }
}