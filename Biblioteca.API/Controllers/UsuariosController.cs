using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        // Se llama a la interface y se pone en modo solo de lectura
        // o lo que seria a ejecutarse una vez
        readonly IUsuarios _usuarios;

        public UsuariosController(IUsuarios usuarios)
        {
            _usuarios = usuarios;
        }

        [HttpPost("GuardarUsuario")]
        public async Task<IActionResult> GuardarUsuario([FromBody] Usuarios usuarios)
        {
            try
            {
                var result = await _usuarios.GuardarUsuario(usuarios);

                if (result.Success)
                {
                    return Ok(new { mensaje = result.Message });
                }
                else
                {
                    return BadRequest(new { mensaje = result.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpGet("ListarUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var resultado = await this._usuarios.ListarUsuarios();
            return Ok(resultado);
        }

        [HttpPut("ActualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuarios usuarios)
        {
            try
            {
                var resultado = await _usuarios.ActualizarUsuario(usuarios);

                if (resultado)
                {
                    return Ok(new { mensaje = "Usuario actualizado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al actualizar el usuario" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpDelete("EliminarUsuario/{usuarioId}")]
        public async Task<IActionResult> EliminarUsuario(int usuarioId)
        {
            try
            {
                var resultado = await _usuarios.EliminarUsuario(usuarioId);

                if (resultado)
                {
                    return Ok(new { mensaje = "Usuario eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar, puede que no exista el usuario" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }
    }
}
