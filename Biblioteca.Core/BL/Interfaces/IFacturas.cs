using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    //Nos permite hacer la abstracción o la herencia de funciones de clases 
    public interface IFacturas
    {
        //Task para devolver asincronos y nos sirve para evitar el colapso del programa 

        Task<bool> GuardarFactura(Facturas facturas);
        Task<bool> ActualizarFactura(Facturas facturas);
        Task<bool> EliminarFactura(int facturaId);
        Task<List<Facturas>> ListarFacturas();
    }
}
