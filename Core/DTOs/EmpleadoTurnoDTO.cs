namespace GYM.Core.DTOs
{
    public class EmpleadoTurnoDTO
    {
        public string ci { get; set; } = null!;
        public string nombre { get; set; } = null!;
        public string apellido_p { get; set; } = null!;
        public string apellido_m { get; set; } = null!;

        public string codigo { get; set; } = null!;
        public string nombreTurno { get; set; } = null!;
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
    }
}