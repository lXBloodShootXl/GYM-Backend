using GYM.Core.Interfaces;
using GYM.Core.Mapeadores;
using Microsoft.AspNetCore.Mvc;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaRepositorio _repositorio;

        public AuditoriaController(IAuditoriaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ListaAuditoria()
        {
            var auditorias = await _repositorio.ListaAuditoria();

            var datos = auditorias
                .Select(AuditoriaMapeador.ToDTO);

            return Ok(datos);
        }
    }
}