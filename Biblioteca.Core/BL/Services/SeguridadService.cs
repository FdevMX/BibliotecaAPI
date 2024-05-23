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
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var usuario = conexion.Usuarios.FirstOrDefault(u => u.name_user == loginData.name_user);

                if (usuario != null)
                {
                    var encryptionService = new EncryptionService();
                    var hashedPassword = encryptionService.HashPassword(loginData.pasword);

                    if (hashedPassword == usuario.pasword)
                    {
                        // La contraseña es correcta
                        return usuario;
                    }
                }

                // La contraseña es incorrecta
                return null;
            }
        }
    }
}
