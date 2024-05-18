using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    // Heredamos los valores de la interface para hacer el contrato
    public class UsuariosService : IUsuarios
    {
        public Task<bool> ActualizarUsuario(Usuarios usuarios)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Usuarios
                                where c.usuario_id == usuarios.usuario_id
                                select c).FirstOrDefault();

                if (consulta != null)
                {
                    consulta.nombre = usuarios.nombre;
                    consulta.apellidos = usuarios.apellidos;
                    consulta.direccion = usuarios.direccion;
                    consulta.ciudad = usuarios.ciudad;
                    consulta.pais = usuarios.pais;
                    consulta.email = usuarios.email;
                    consulta.telefono = usuarios.telefono;
                    consulta.rol_usuario = usuarios.rol_usuario;
                    consulta.name_user = usuarios.name_user;
                    consulta.pasword = usuarios.pasword;

                    result = conexion.SaveChanges() > 0;
                }
            }

            return Task.FromResult(result);
        }

        public async Task<bool> EliminarUsuario(int usuarioId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Usuarios.FirstOrDefault(c => c.usuario_id == usuarioId);
                if (consulta != null)
                {
                    conexion.Usuarios.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;
        }

        public async Task<OperationResult> GuardarUsuario(Usuarios usuarios)
        {
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consultaEmail = conexion.Usuarios.FirstOrDefault(c => c.email == usuarios.email);
                var consultaNameUser = conexion.Usuarios.FirstOrDefault(c => c.name_user == usuarios.name_user);

                if (consultaEmail != null)
                {
                    return new OperationResult { Success = false, Message = "El email ya está en uso" };
                }

                if (consultaNameUser != null)
                {
                    return new OperationResult { Success = false, Message = "El nombre de usuario ya está en uso" };
                }

                Usuarios nuevoUsuario = new Usuarios
                {
                    nombre = usuarios.nombre,
                    apellidos = usuarios.apellidos,
                    direccion = usuarios.direccion,
                    ciudad = usuarios.ciudad,
                    pais = usuarios.pais,
                    email = usuarios.email,
                    telefono = usuarios.telefono,
                    rol_usuario = usuarios.rol_usuario,
                    name_user = usuarios.name_user,
                    pasword = usuarios.pasword
                };

                conexion.Usuarios.Add(nuevoUsuario);
                var resultado = await conexion.SaveChangesAsync() > 0;

                return new OperationResult { Success = resultado, Message = resultado ? "Usuario guardado correctamente" : "Error al guardar el usuario" };
            }
        }

        /*
        public Task<bool> GuardarUsuario(Usuarios usuarios)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Usuarios
                                where c.usuario_id == usuarios.usuario_id
                                select c).FirstOrDefault();

                if (consulta == null)
                {
                    Usuarios user = new Usuarios();

                    user.nombre = usuarios.nombre;
                    user.apellidos = usuarios.apellidos;
                    user.direccion = usuarios.direccion;
                    user.ciudad = usuarios.ciudad;
                    user.pais = usuarios.pais;
                    user.email = usuarios.email;
                    user.telefono = usuarios.telefono;
                    user.rol_usuario = usuarios.rol_usuario;
                    user.name_user = usuarios.name_user;
                    user.pasword = usuarios.pasword;

                    conexion.Usuarios.Add(user);
                    result = conexion.SaveChanges() > 0;
                }
            }

            return Task.FromResult(result);
        }
        */

        public Task<List<Usuarios>> ListarUsuarios()
        {
            List<Usuarios> list = new List<Usuarios>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Usuarios
                                select c).ToList();

                foreach (var item in consulta)
                {
                    list.Add(new Usuarios()
                    {
                        usuario_id = item.usuario_id,
                        nombre = item.nombre,
                        apellidos = item.apellidos,
                        direccion = item.direccion,
                        ciudad = item.ciudad,
                        pais = item.pais,
                        email = item.email,
                        telefono = item.telefono,
                        rol_usuario = item.rol_usuario,
                        name_user = item.name_user,
                        pasword = item.pasword
                    });
                }

                return Task.FromResult(list);

            }

        }
    }
}
