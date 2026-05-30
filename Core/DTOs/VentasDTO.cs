namespace DTOS
{
    public class VentasDTO
    {
        public string codigo { get; set; } = null!;

        public DateTime fecha { get; set; }

        public string ciEmpleado { get; set; } = null!;

        public string ciCliente { get; set; } = null!;
    }
}