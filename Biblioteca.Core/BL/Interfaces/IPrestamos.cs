using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    public interface IPrestamos
    {
        Task<bool> GuardarPrestamo(Prestamos prestamos);
        Task<bool> ActualizarPrestamo(Prestamos prestamos);
        Task<bool> EliminarPrestamo(int prestamoId);
        Task<List<Prestamos>> ListarPrestamos();
    }
}
