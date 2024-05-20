using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class DevolucionesService : IDevoluciones
    {
        public Task<bool> ActualizarDevolucion(Devoluciones devoluciones)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Devoluciones
                                where c.devolucion_id == devoluciones.devolucion_id
                                select c).FirstOrDefault();
                if (consulta != null)
                {
                    consulta.prestamo_id = devoluciones.prestamo_id;
                    consulta.fecha_devolucion = devoluciones.fecha_devolucion;
                    consulta.estado_libro = devoluciones.estado_libro;
                    consulta.observaciones = devoluciones.observaciones;

                    result = conexion.SaveChanges() > 0;

                }

            }
            return Task.FromResult(result);
        }

        public async Task<bool> EliminarDevolucion(int devolucionId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Devoluciones.FirstOrDefault(c => c.devolucion_id == devolucionId);
                if (consulta != null)
                {
                    conexion.Devoluciones.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;
        }

        public Task<bool> GuardarDevolucion(Devoluciones devoluciones)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Devoluciones
                                where c.devolucion_id == devoluciones.devolucion_id
                                select c).FirstOrDefault();

                if (consulta == null)
                {
                    Devoluciones Dev = new Devoluciones();

                    Dev.prestamo_id = devoluciones.prestamo_id;
                    Dev.fecha_devolucion = devoluciones.fecha_devolucion;
                    Dev.estado_libro = devoluciones.estado_libro;
                    Dev.observaciones = devoluciones.observaciones;

                    conexion.Devoluciones.Add(Dev);
                    result = conexion.SaveChanges() > 0;

                }

            }
            return Task.FromResult(result);

        }

        public Task<List<Devoluciones>> ListarDevoluciones()
        {
            List<Devoluciones> list = new List<Devoluciones>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Devoluciones
                                select c).ToList();
                foreach (var item in consulta)
                {
                    list.Add(new Devoluciones()
                    {
                        devolucion_id = item.devolucion_id,
                        prestamo_id = item.prestamo_id,
                        fecha_devolucion = item.fecha_devolucion,
                        estado_libro = item.estado_libro,
                        observaciones = item.observaciones,
                    });

                }
                return Task.FromResult(list);
            }
        }
    }
}
