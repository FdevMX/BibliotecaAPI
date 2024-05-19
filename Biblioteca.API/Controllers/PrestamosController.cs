using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Prestamos")]
    public class PrestamosController : ControllerBase
    {
        readonly IPrestamos _prestamos;

        public PrestamosController(IPrestamos prestamos)
        {
            _prestamos = prestamos;
        }

        [HttpPost("GuardarPrestamo")]
        public async Task<IActionResult> GuardarPrestamo([FromBody] Prestamos prestamos)
        {
            try
            {
                var resultado = await _prestamos.GuardarPrestamo(prestamos);
                
                if (resultado)
                {
                    return Ok(new { mensaje = "Prestamo agregado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al agregar el prestamo" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpGet("ListarPrestamos")]
        public async Task<IActionResult> ListarPrestamos()
        {
            var resultado = await this._prestamos.ListarPrestamos();
            return Ok(resultado);
        }

        [HttpPut("ActualizarPrestamo")]
        public async Task<IActionResult> ActualizarPrestamo([FromBody] Prestamos prestamos)
        {
            try
            {
                var resultado = await _prestamos.ActualizarPrestamo(prestamos);

                if (resultado)
                {
                    return Ok(new { mensaje = "Prestamo actualizado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al actualizar el prestamo" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpDelete("EliminarPrestamo/{prestamoId}")]
        public async Task<IActionResult> EliminarPrestamo(int prestamoId)
        {
            try
            {
                var resultado = await _prestamos.EliminarPrestamo(prestamoId);

                if (resultado)
                {
                    return Ok(new { mensaje = "Prestamo eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar el prestamo" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }
    }
}
