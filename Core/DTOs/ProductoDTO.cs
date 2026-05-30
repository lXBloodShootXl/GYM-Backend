namespace GYM.Core.DTOs
{
    public class ProductoDTO
    {
        public string codigo { get; set; } = null!;

        public string nombre { get; set; } = null!;

        public string descripcion { get; set; } = null!;

        public decimal precio { get; set; }

        public string codigoCategoria { get; set; } = null!;
    }
}