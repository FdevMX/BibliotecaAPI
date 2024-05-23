using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Editoriales")]
    public class EditorialesController : ControllerBase
    {
        readonly IEditoriales _editoriales;
        public EditorialesController(IEditoriales editoriales)
        {
            _editoriales = editoriales;

        }

        [HttpPost("GuardarEditorial")]
        public async Task<IActionResult> GuardarEditorial([FromBody] Editoriales editoriales)
        {
            try
            {
                var resultado = await _editoriales.GuardarEditorial(editoriales);

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

        [HttpGet("ListarEditorial")]
        public async Task<IActionResult> ListarEditorial()
        {
            var resultado = await this._editoriales.ListarEditoriales();
            return Ok(resultado);

        }

        [HttpPut("ActualizarEditorial")]
        public async Task<IActionResult> ActualizarEditorial([FromBody] Editoriales editoriales)
        {
            try
            {
                var resultado = await _editoriales.ActualizarEditorial(editoriales);

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
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message} " });
            }


        }

        [HttpDelete("EliminarEditorial/{editorialid}")]
        public async Task<IActionResult> EliminarEditorial(int editorialid)
        {
            try
            {
                var resultado = await _editoriales.EliminarEditorial(editorialid);

                if (resultado)
                {
                    return Ok(new { mensaje = "Editorial eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar el editorial" });
                }

            }
            catch (Exception ex)

            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message} " });
            }
        }

    }

}
