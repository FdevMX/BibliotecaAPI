using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Devoluciones")]
    public class DevolucionesController : ControllerBase
    {
        readonly IDevoluciones _devoluciones;
        public DevolucionesController(IDevoluciones devoluciones)
        {
            _devoluciones = devoluciones;
        }

        [HttpPost("GuardarDevoluciones")]
        public async Task<IActionResult> GuardarDevoluciones([FromBody] Devoluciones devoluciones)
        {
            try
            {
                var resultado = await _devoluciones.GuardarDevolucion(devoluciones);

                if (resultado)
                {
                    return Ok(new { mensaje = "Devolucion agregado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error en la devolucion " });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpGet("ListarDevoluciones")]
        public async Task<IActionResult> ListarDevoluciones()
        {
            var resultado = await this._devoluciones.ListarDevoluciones();
            return Ok(resultado);
        }

        [HttpPut("ActualizarDevolucion")]
        public async Task<IActionResult> ActualizarDevolucion([FromBody] Devoluciones devoluciones)
        {
            try
            {
                var resultado = await _devoluciones.ActualizarDevolucion(devoluciones);

                if (resultado)
                {
                    return Ok(new { mensaje = " La devolucion actualizado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al actualizar la devolucion" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpDelete("EliminarDevolucion/{devolucionId}")]
        public async Task<IActionResult> EliminarDevolucion(int devolucionId)
        {
            try
            {
                var resultado = await _devoluciones.EliminarDevolucion(devolucionId);

                if (resultado)
                {
                    return Ok(new { mensaje = "Devolucion eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar la devolucion" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }

        }
    }
}
