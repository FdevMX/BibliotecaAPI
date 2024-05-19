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
        public Task<bool> ActualizarAutor(Autores autores)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Autores
                                where c.autor_id == autores.autor_id
                                select c).FirstOrDefault();
                if (consulta != null)
                {
                    consulta.nombre = autores.nombre;
                    consulta.apellidos = autores.apellidos;
                    consulta.nacimiento = autores.nacimiento;
                    consulta.nacionalidad = autores.nacionalidad;
                    consulta.biografia = autores.biografia;

                    result = conexion.SaveChanges() > 0;

                }

            }
            return Task.FromResult(result);
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

        public Task<bool> GuardarAutor(Autores autores)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Autores
                                where c.autor_id == autores.autor_id
                                select c).FirstOrDefault();

                if (consulta == null)
                {
                    Autores aut = new Autores();

                    aut.nombre = autores.nombre;
                    aut.apellidos = autores.apellidos;
                    aut.nacimiento = autores.nacimiento;
                    aut.nacionalidad = autores.nacionalidad;
                    aut.biografia = autores.biografia;

                    conexion.Autores.Add(aut);
                    result = conexion.SaveChanges() > 0;

                }

            }
            return Task.FromResult(result);

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
