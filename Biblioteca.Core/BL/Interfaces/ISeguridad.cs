using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Interfaces
{
    public interface ISeguridad
    {
        //Sera una tarea porque permite los metodos async y await
        Task<Usuarios> IniciarSesion(LoginData loginData);
    }
}
