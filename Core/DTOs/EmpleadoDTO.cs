namespace GYM.Core.DTOs
{
    public class EmpleadoDTO
    {
        public string nombre { get; set; } = null!;
        public string ci { get; set; } = null!;
        public string? apellido_p { get; set; } = null;
        public string? apellido_m { get; set; } = null;
        public DateOnly fecha { get; set; }
    }
}
