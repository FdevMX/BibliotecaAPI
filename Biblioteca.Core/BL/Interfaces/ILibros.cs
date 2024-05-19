using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    // Nos permite hacer la abstraccion,
    // o la herencia de funciones entre clases
    public interface ILibros
    {
        // Task devuelve valores asyncronos y
        // nos sirve para evitar el colapso del programa
        Task<bool> GuardarLibro(Libros libros);
        Task<bool> ActualizarLibro(Libros libros);
        Task<bool> EliminarLibro(int libroId);
        Task<List<Libros>> ListarLibros();
    }
}
