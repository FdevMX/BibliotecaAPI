using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Generos")]
    public class GenerosController : ControllerBase
    {
        readonly IGeneros _generos;

        public GenerosController(IGeneros generos)
        {
            _generos = generos;
        }

        [HttpPost("GuardarGenero")]
        public async Task<IActionResult> GuardarGenero([FromBody] Generos generos)
        {
            var resultado = await _generos.GuardarGenero(generos);
            if (resultado == false)
            {
                return BadRequest(resultado);
            }
            return Ok(resultado);
        }

        [HttpGet("ListarGeneros")]
        public async Task<IActionResult> ListarGeneros()
        {
            var resultado = await this._generos.ListarGeneros();
            return Ok(resultado);
        }

        [HttpPut("ActualizarGenero")]
        public async Task<IActionResult> ActualizarGenero([FromBody] Generos genero)
        {
            try
            {
                var resultado = await _generos.ActualizarGenero(genero);

                if (resultado)
                {
                    return Ok(new { mensaje = "Genero actualizado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al actualizar libro" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpDelete("EliminarGenero/{generoId}")]
        public async Task<IActionResult> EliminarGenero(int generoId)
        {
            try
            {
                var resultado = await _generos.EliminarGenero(generoId);

                if (resultado)
                {
                    return Ok(new { mensaje = "Genero eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar libro" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }






        }
    }
}
