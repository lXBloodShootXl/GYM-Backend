namespace GYM.Core.DTOs
{
    public class StockDTO
    {
        public string codigoInventario { get; set; } = null!;

        public string codigoProducto { get; set; } = null!;

        public int cantidad { get; set; }
    }
}