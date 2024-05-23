using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class EditorialesService : IEditoriales
    {
        public Task<OperationResult> ActualizarEditorial(Editoriales editoriales)
        {
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Editoriales
                                where c.editorial_id == editoriales.editorial_id
                                select c).FirstOrDefault();

                if (consulta != null)
                {
                    var consultaDuplicado = conexion.Editoriales.FirstOrDefault(
                        c => c.nombre == editoriales.nombre &&
                        c.direccion == editoriales.direccion &&
                        c.ciudad == editoriales.ciudad &&
                        c.pais == editoriales.pais &&
                        c.editorial_id != editoriales.editorial_id);
                    

                    if (consultaDuplicado != null)
                    {
                        return new OperationResult { Success = false, Message = "Ya existe un editorial con los mismos datos" };
                    }

                    consulta.nombre = editoriales.nombre;
                    consulta.direccion = editoriales.direccion;
                    consulta.ciudad = editoriales.ciudad;
                    consulta.pais = editoriales.pais;
                    consulta.descripcion = editoriales.descripcion;

                    var resultado = await conexion.SaveChangesAsync() > 0;
                    return new OperationResult { Success = resultado, Message = resultado ? "Editorial actualizado correctamente" : "Error al actualizar el editorial" };                   
                }

                return new OperationResult { Success = false, Message = "No se encontró el editorial a actualizar" };
            }
        }

        public async Task<bool> EliminarEditorial(int editorialId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Editoriales.FirstOrDefault(c => c.editorial_id == editorialId);
                if (consulta != null)
                {
                    conexion.Editoriales.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;
        }

        public Task<bool> GuardarEditorial(Editoriales editoriales)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Editoriales
                                where c.editorial_id == editoriales.editorial_id
                                select c).FirstOrDefault();

                if (consulta == null)
                {
                    Editoriales edit = new Editoriales();

                    edit.nombre = editoriales.nombre;
                    edit.direccion = editoriales.direccion;
                    edit.ciudad = editoriales.ciudad;
                    edit.pais = editoriales.pais;
                    edit.descripcion = editoriales.descripcion;

                    conexion.Editoriales.Add(edit);
                    result = conexion.SaveChanges() > 0;
                }

            }
            return Task.FromResult(result);
        }

        public Task<List<Editoriales>> ListarEditoriales()
        {
            List<Editoriales> list = new List<Editoriales>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Editoriales
                                select c).ToList();

                foreach (var item in consulta)
                {
                    list.Add(new Editoriales()
                    {
                        editorial_id = item.editorial_id,
                        nombre = item.nombre,
                        direccion = item.direccion,
                        ciudad = item.ciudad,
                        pais = item.pais,
                        descripcion = item.descripcion
                    });



                }

            }
            return Task.FromResult(list);
        }
    }
}
