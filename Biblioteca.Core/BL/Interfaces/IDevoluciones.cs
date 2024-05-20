using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    public interface IDevoluciones
    {
        Task<bool> GuardarDevolucion(Devoluciones devoluciones);
        Task<bool> ActualizarDevolucion(Devoluciones devoluciones);
        Task<bool> EliminarDevolucion(int devolucionId);
        Task<List<Devoluciones>> ListarDevoluciones();
    }
}
