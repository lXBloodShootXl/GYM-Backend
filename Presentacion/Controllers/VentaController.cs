using DTOS;
using GYM.Infraestructura.Data;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaRepositorio _repositorio;
        private readonly GYM_DBContext _context;

        public VentaController(
            IVentaRepositorio repositorio,
            GYM_DBContext context)
        {
            _repositorio = repositorio;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListaVentas()
        {
            var ventas = await _repositorio.ListaVentas();

            return Ok(ventas);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string codigo, DateTime fecha, string ciEmpleado, string ciCliente)
        {
            var empleado = await _context.Empleados
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e => e.persona.ci == ciEmpleado);

            var cliente = await _context.Clientes
                .Include(c => c.persona)
                .FirstOrDefaultAsync(c => c.persona.ci == ciCliente);

            if (empleado == null || cliente == null)
            {
                return BadRequest("Empleado o Cliente no existe");
            }

            Ventas venta = new Ventas
            {
                codigo = codigo,
                fecha = fecha,
                id_empleado = empleado.id_empleado,
                id_cliente = cliente.id_cliente
            };

            await _repositorio.Post(venta);

            return Ok();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            await _repositorio.Delete(codigo);

            return Ok();
        }
    }
}