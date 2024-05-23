using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Autores")]
    public class AutoresController : ControllerBase
    {
        readonly IAutores _autores;

        public AutoresController(IAutores autores)
        {
            _autores = autores;
        }

        [HttpPost("GuardarAutor")]
        public async Task<IActionResult> GuardarAutor([FromBody] Autores autores)
        {
            try
            {
                var resultado = await _autores.GuardarAutor(autores);

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

        [HttpGet("ListarAutores")]
        public async Task<IActionResult> ListarAutores()
        {
            var resultado = await this._autores.ListarAutores();
            return Ok(resultado);
        }

        [HttpPut("ActualizarAutor")]
        public async Task<IActionResult> ActualizarAutor([FromBody] Autores autores)
        {
            try
            {
                var resultado = await _autores.ActualizarAutor(autores);

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

        [HttpDelete("EliminarAutor/{autorId}")]
        public async Task<IActionResult> EliminarAutor(int autorId)
        {
            try
            {
                var resultado = await _autores.EliminarAutor(autorId);

                if (resultado)
                {
                    return Ok(new { mensaje = "Autor eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar el autor" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }

        }

    }

}
