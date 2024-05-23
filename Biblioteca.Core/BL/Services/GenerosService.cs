using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class GenerosService : IGeneros
    {
        public async Task<OperationResult> ActualizarGenero(Generos generos)
        {
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Generos
                                where c.genero_id == generos.genero_id
                                select c).FirstOrDefault();
                if (consulta != null)
                {
                    var consultaDuplicado = conexion.Generos.FirstOrDefault(
                        c => c.nombre == generos.nombre &&
                             c.genero_id != generos.genero_id);

                    if ( consultaDuplicado != null)
                    {
                        return new OperationResult { Success = false, Message = "Ya existe un genero con los mismos datos" };
                    }
                    
                    consulta.nombre = generos.nombre;
                    consulta.descripcion = generos.descripcion;

                    var resultado = await conexion.SaveChangesAsync() > 0;
                    return new OperationResult { Success = resultado, Message = resultado ? "Genero actualizado correctamente" : "Error al actualizar el genero" };

                }
                return new OperationResult { Success = false, Message = "No se encontró el genero a actualizar" };
            }
        }

        public async Task<bool> EliminarGenero(int generoId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Generos.FirstOrDefault(c => c.genero_id == generoId);
                if (consulta != null)
                {
                    conexion.Generos.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;
        }

        public async Task<OperationResult> GuardarGenero(Generos generos)
        {
            using(var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Generos.FirstOrDefault(
                    c => c.nombre == generos.nombre);
                
                if (consulta != null)
                {
                    return new OperationResult { Success = false, Message = "Ya existe un genero con los mismos datos" };
                }
                
                Generos gen = new Generos
                {
                    nombre = generos.nombre,
                    descripcion = generos.descripcion
                };
                
                conexion.Generos.Add(gen);
                var resultado = await conexion.SaveChangesAsync() > 0;

                return new OperationResult
                {
                    Success = resultado,
                    Message = resultado ? "Genero agregado correctamente" : "Error al agregar el genero"
                };
            }
        }

        public Task<List<Generos>> ListarGeneros()
        {
            List<Generos> list = new List<Generos>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Generos
                                select c).ToList();
                foreach (var item in consulta)

                {
                    list.Add(new Generos()
                    {
                        genero_id = item.genero_id,
                        nombre = item.nombre,
                        descripcion = item.descripcion

                    });

                }

                return Task.FromResult(list);
            }
        }
    }
}
