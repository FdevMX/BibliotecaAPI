using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class AutoresService : IAutores
    {
        public async Task<OperationResult> ActualizarAutor(Autores autores)
        {
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Autores
                                where c.autor_id == autores.autor_id
                                select c).FirstOrDefault();

                if (consulta != null)
                {
                    var consultaDuplicado = conexion.Autores.FirstOrDefault(
                        c => c.nombre == autores.nombre &&
                             c.apellidos == autores.apellidos &&
                             c.nacimiento == autores.nacimiento &&
                             c.nacionalidad == autores.nacionalidad &&
                             c.autor_id != autores.autor_id);

                    if (consultaDuplicado != null)
                    {
                        return new OperationResult { Success = false, Message = "Ya existe un autor con los mismos datos" };
                    }

                    consulta.nombre = autores.nombre;
                    consulta.apellidos = autores.apellidos;
                    consulta.nacimiento = autores.nacimiento;
                    consulta.nacionalidad = autores.nacionalidad;
                    consulta.biografia = autores.biografia;

                    var resultado = await conexion.SaveChangesAsync() > 0;
                    return new OperationResult { Success = resultado, Message = resultado ? "Autor actualizado correctamente" : "Error al actualizar el autor" };
                }

                return new OperationResult { Success = false, Message = "No se encontró el autor a actualizar" };
            }
        }

        public async Task<bool> EliminarAutor(int autorId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Autores.FirstOrDefault(c => c.autor_id == autorId);
                if (consulta != null)
                {
                    conexion.Autores.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;
        }

        public async Task<OperationResult> GuardarAutor(Autores autores)
        {
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Autores.FirstOrDefault(
                    c => c.nombre == autores.nombre &&
                         c.apellidos == autores.apellidos &&
                         c.nacimiento == autores.nacimiento &&
                         c.nacionalidad == autores.nacionalidad);

                if (consulta != null)
                {
                    return new OperationResult { Success = false, Message = "Ya existe un autor con los mismos datos" };
                }

                Autores aut = new Autores
                {
                    nombre = autores.nombre,
                    apellidos = autores.apellidos,
                    nacimiento = autores.nacimiento,
                    nacionalidad = autores.nacionalidad,
                    biografia = autores.biografia
                };

                conexion.Autores.Add(aut);
                var resultado = await conexion.SaveChangesAsync() > 0;

                return new OperationResult { Success = resultado, Message = resultado ? "Autor agregado correctamente" : "Error al agregar el autor" };
            }
        }

        public Task<List<Autores>> ListarAutores()
        {
            List<Autores> list = new List<Autores>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Autores
                                select c).ToList();
                foreach (var item in consulta)
                {
                    list.Add(new Autores()
                    {
                        autor_id = item.autor_id,
                        nombre = item.nombre,
                        apellidos = item.apellidos,
                        nacimiento = item.nacimiento,
                        nacionalidad = item.nacionalidad,
                        biografia = item.biografia
                    });

                }
                return Task.FromResult(list);
            }
        }
    }
}
