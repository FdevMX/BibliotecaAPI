using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Api/Seguridad")]
    public class SeguridadController : ControllerBase
    {
        readonly ISeguridad BLSeguridad;

        //Se utiliza inyecccion de dependencia
        public SeguridadController(ISeguridad seguridad)
        {
            this.BLSeguridad = seguridad;
        }

        /*[HttpGet("IniciarSesion")]
        public IActionResult IniciarSesion(string Usuario, string Password)
        {
            return Ok("Acceso correcto");
        }*/

        [HttpGet("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(string Usuario, string Password)
        {
            var loginData = new LoginData
            {
                name_user = Usuario,
                pasword = Password
            };

            var resultado = await this.BLSeguridad.IniciarSesion(loginData);

            if (resultado != null)
            {
                // El usuario existe, puedes procesar la información del usuario aquí
                //return Ok(resultado);
                return Ok(new
                {
                    mensaje = "Inicio de sesion exitoso",
                    datosUsuario = resultado
                });
            }
            else
            {
                // El usuario no existe o las credenciales son incorrectas
                //return Unauthorized();
                return Unauthorized(new
                {
                    mensaje = "Usuario o contraseña incorrectos"
                });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            var resultado = await this.BLSeguridad.IniciarSesion(loginData);
            return Ok(resultado);
        }
    }
}
