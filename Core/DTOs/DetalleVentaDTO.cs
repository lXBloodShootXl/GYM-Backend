namespace GYM.Core.DTOs
{
    public class DetalleVentaDTO
    {
        public string codigoVenta { get; set; } = null!;

        public string codigoProducto { get; set; } = null!;

        public int cantidad { get; set; }
    }
}
