using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    //nos permite la abtraccion 
    //o la herencia de funciones entre clases
    public interface IEditoriales
    //task devuenve valores asyncronos y 
    //nos sirve para eviar un colapso del programa
    {
        Task<OperationResult> GuardarEditorial(Editoriales editoriales);
        Task<OperationResult> ActualizarEditorial(Editoriales editoriales);
        Task<bool> EliminarEditorial(int editoriales_id);
        Task<List<Editoriales>> ListarEditoriales();
    }
}
