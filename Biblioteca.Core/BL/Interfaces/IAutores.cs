using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    //Nos permite hacer la abstracción o la herencia de funciones de clases 
    public interface IAutores
    {
        //Task para devolver asincronos y nos sirve para evitar el colapso del programa 
        Task<bool> GuardarAutor(Autores autores);
        Task<bool> ActualizarAutor(Autores autores);
        Task<bool> EliminarAutor(int AutorId);
        Task<List<Autores>> ListarAutores();
    }
}
