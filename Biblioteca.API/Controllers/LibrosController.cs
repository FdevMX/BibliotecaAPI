using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Libros")]
    public class LibrosController : ControllerBase
    {
        readonly ILibros _libros;

        public LibrosController(ILibros libros)
        {
            _libros = libros;
        }

        [HttpPost("GuardarLibro")]
        public async Task<IActionResult> GuardarLibro([FromBody] Libros libros)
        {
            try
            {
                var resultado = await _libros.GuardarLibro(libros);

                if (resultado.Success)
                {
                    return Ok(new { mensaje = resultado.Message });
                }
                else
                {
                    return BadRequest(new { mensaje = resultado.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpGet("ListarLibros")]
        public async Task<IActionResult> ListarLibros()
        {
            var resultado = await this._libros.ListarLibros();
            return Ok(resultado);
        }

        [HttpPut("ActualizarLibro")]
        public async Task<IActionResult> ActualizarLibro([FromBody] Libros libros)
        {
            try
            {
                var resultado = await _libros.ActualizarLibro(libros);

                if (resultado.Success)
                {
                    return Ok(new { mensaje = resultado.Message });
                }
                else
                {
                    return BadRequest(new { mensaje = resultado.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpDelete("EliminarLibro/{libroId}")]
        public async Task<IActionResult> EliminarLibro(int libroId)
        {
            try
            {
                var resultado = await _libros.EliminarLibro(libroId);

                if (resultado)
                {
                    return Ok(new { mensaje = "Libro eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar el libro" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }
    }
}
