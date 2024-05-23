using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class LibrosService : ILibros
    {
        public async Task<OperationResult> ActualizarLibro(Libros libros)
        {
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Libros
                                where c.libro_id == libros.libro_id
                                select c).FirstOrDefault();

                if (consulta != null)
                {
                    var consultaISBN = conexion.Libros.FirstOrDefault(
                        c => c.isbn == libros.isbn &&
                             c.libro_id != libros.libro_id);

                    if (consultaISBN != null)
                    {
                        return new OperationResult { Success = false, Message = "El ISBN ya está en uso por otro libro" };
                    }

                    consulta.titulo = libros.titulo;
                    consulta.isbn = libros.isbn;
                    consulta.descripcion = libros.descripcion;
                    consulta.anio_pub = libros.anio_pub;
                    consulta.num_paginas = libros.num_paginas;
                    consulta.idioma = libros.idioma;
                    consulta.estado = libros.estado;
                    consulta.autor_id = libros.autor_id;
                    consulta.editorial_id = libros.editorial_id;
                    consulta.genero_id = libros.genero_id;
                    consulta.copy_dispo = libros.copy_dispo;

                    var resultado = await conexion.SaveChangesAsync() > 0;
                    return new OperationResult { Success = resultado, Message = resultado ? "Libro actualizado correctamente" : "Error al actualizar el libro" };
                }

                return new OperationResult { Success = false, Message = "No se encontró el libro a actualizar" };
            }
        }

        public async Task<bool> EliminarLibro(int libroId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Libros.FirstOrDefault(c => c.libro_id == libroId);
                if (consulta != null)
                {
                    conexion.Libros.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;
        }

        public async Task<OperationResult> GuardarLibro(Libros libros)
        {
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consultaISBN = conexion.Libros.FirstOrDefault(c => c.isbn == libros.isbn);

                if (consultaISBN != null)
                {
                    return new OperationResult { Success = false, Message = "Cuidado! ⚠️. Hay un libro con el mismo ISBN" };
                }

                Libros lib = new Libros
                {
                    titulo = libros.titulo,
                    isbn = libros.isbn,
                    descripcion = libros.descripcion,
                    anio_pub = libros.anio_pub,
                    num_paginas = libros.num_paginas,
                    idioma = libros.idioma,
                    estado = libros.estado,
                    autor_id = libros.autor_id,
                    editorial_id = libros.editorial_id,
                    genero_id = libros.genero_id,
                    copy_dispo = libros.copy_dispo
                };

                conexion.Libros.Add(lib);
                var resultado = await conexion.SaveChangesAsync() > 0;

                return new OperationResult { Success = resultado, Message = resultado ? "Libro agregado correctamente" : "Error al agregar el libro" };
            }
        }

        public Task<List<Libros>> ListarLibros()
        {
            List<Libros> list = new List<Libros>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Libros
                                select c).ToList();

                foreach (var item in consulta)
                {
                    list.Add(new Libros()
                    {
                        libro_id = item.libro_id,
                        titulo = item.titulo,
                        isbn = item.isbn,
                        descripcion = item.descripcion,
                        anio_pub = item.anio_pub,
                        num_paginas = item.num_paginas,
                        idioma = item.idioma,
                        estado = item.estado,
                        autor_id = item.autor_id,
                        editorial_id = item.editorial_id,
                        genero_id = item.genero_id,
                        copy_dispo = item.copy_dispo
                    });
                }

                return Task.FromResult(list);
            }
        }
    }
}
