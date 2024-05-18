using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class SeguridadService : ISeguridad
    {
        public async Task<Usuarios> IniciarSesion(LoginData loginData)
        {
            //Usuarios? consulta = new Usuarios();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Usuarios
                                where c.name_user == loginData.name_user &&
                                      c.pasword == loginData.pasword
                                select c).FirstOrDefault();

                //return await Task.FromResult(consulta ?? new Usuarios());
                return await Task.FromResult(consulta);
            }
        }

    }
}
