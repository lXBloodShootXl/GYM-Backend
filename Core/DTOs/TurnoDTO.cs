namespace GYM.Core.DTOs
{
    public class TurnoDTO
    {
        public string codigo { get; set; } = null!;
        public string nombre { get; set; } = null!;
        public string hora_fin { get; set; }
        public string hora_inicio { get; set; }
    }
}
