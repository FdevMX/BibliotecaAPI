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
        public Task<bool> ActualizarGenero(Generos generos)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Generos
                                where c.genero_id == generos.genero_id
                                select c).FirstOrDefault();
                if (consulta != null)

                {
                    consulta.genero_id = generos.genero_id;
                    consulta.nombre = generos.nombre;
                    consulta.descripcion = generos.descripcion;

                    result = conexion.SaveChanges() > 0;

                }
            }
            return Task.FromResult(result);
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

        public Task<bool> GuardarGenero(Generos generos)
        {
            bool result = false;
            using(var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Generos
                                where c.genero_id == generos.genero_id
                                select c).FirstOrDefault();
                if (consulta == null)
                {
                    Generos gen = new Generos();

                    
                    gen.nombre = generos.nombre;
                    gen.descripcion = generos.descripcion;

                    conexion.Generos.Add(gen);
                    result = conexion.SaveChanges() > 0;

                }
            }
            return Task.FromResult(result);
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
