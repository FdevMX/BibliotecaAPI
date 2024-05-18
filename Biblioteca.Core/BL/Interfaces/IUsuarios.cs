using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    // Las interfaces nos permite la abstraccion
    // de codigo para luego hacer un contrato en services (usuarioservices)
    // sirven para permitir la herencia de funciones entre clases
    public interface IUsuarios
    {

        // Se usa task en el codigo para devolver valores asyncronos
        // Se creo para evitar el collapso del programa cuando se trabaja paralelamente
        //Task<bool> GuardarUsuario(Models.Usuarios usuarios);
        Task<OperationResult> GuardarUsuario(Usuarios usuarios);
        Task<bool> ActualizarUsuario(Models.Usuarios usuarios);
        //Task<bool> EliminarUsuario(Models.Usuarios usuarios);
        Task<bool> EliminarUsuario(int usuarioId);
        Task<List<Models.Usuarios>> ListarUsuarios();
    }
}
