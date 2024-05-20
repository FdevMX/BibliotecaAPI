using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    public class FacturasController : ControllerBase
    {
        readonly IFacturas _facturas;
        public FacturasController(IFacturas facturas)
        {
            _facturas = facturas;
        }

        [HttpPost("GuardarFactura")]

        public async Task<IActionResult> GuardarFactura([FromBody] Facturas facturas)
        {
            try
            {
                var resultado = await _facturas.GuardarFactura(facturas);

                if (resultado)
                {
                    return Ok(new { mensaje = "Factura agregada correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al agregar la factura" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpGet("ListarFacturas")]
        public async Task<IActionResult> ListarFacturas()
        {
            var resultado = await this._facturas.ListarFacturas();
            return Ok(resultado);
        }

        [HttpPut("ActualizarFactura")]
        public async Task<IActionResult> ActualizarFactura([FromBody] Facturas facturas)
        {
            try
            {
                var resultado = await _facturas.ActualizarFactura(facturas);

                if (resultado)
                {
                    return Ok(new { mensaje = "Factura actualizada correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al actualizar la factura" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpDelete("EliminarFactura/{facturaId}")]
        public async Task<IActionResult> EliminarFactura(int facturaId)
        {
            try
            {
                var resultado = await _facturas.EliminarFactura(facturaId);

                if (resultado)
                {
                    return Ok(new { mensaje = "Factura eliminada correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al eliminar la factura" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error inesperado: {ex.Message}" });
            }

        }
    }
}
