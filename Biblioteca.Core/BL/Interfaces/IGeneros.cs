using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces

{ 
    // public interface nos permite hacer la abstraccion, o la herencia de funciones entre clases
    public interface IGeneros
    {
        // Task para devolver asyncronos y nos sirve para evitar el colapso del programa
        Task<bool> GuardarGenero(Generos generos);
        Task<bool> ActualizarGenero(Generos generos);
        Task<bool> EliminarGenero(int generoId);
        Task<List<Generos>> ListarGeneros();
    }
}
