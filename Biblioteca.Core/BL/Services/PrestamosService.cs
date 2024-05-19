using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class PrestamosService : IPrestamos
    {
        public Task<bool> ActualizarPrestamo(Prestamos prestamos)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Prestamos
                                where c.prestamo_id == prestamos.prestamo_id
                                select c).FirstOrDefault();

                if (consulta != null)
                {
                    consulta.libro_id = prestamos.libro_id;
                    consulta.usuario_id = prestamos.usuario_id;
                    consulta.fecha_prestamo = prestamos.fecha_prestamo;
                    consulta.fecha_devolucion = prestamos.fecha_devolucion;
                    consulta.estado_prestamo = prestamos.estado_prestamo;
                  
                    result = conexion.SaveChanges() > 0;
                }
            }
            return Task.FromResult(result);
        }

        public async Task<bool> EliminarPrestamo(int prestamoId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Prestamos.FirstOrDefault(c => c.prestamo_id == prestamoId);
                if (consulta != null)
                {
                    conexion.Prestamos.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;
        }

        public Task<bool> GuardarPrestamo(Prestamos prestamos)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Prestamos
                                where c.prestamo_id == prestamos.prestamo_id
                                select c).FirstOrDefault();

                if (consulta == null)
                {
                    Prestamos pres = new Prestamos();

                    pres.libro_id = prestamos.libro_id;
                    pres.usuario_id = prestamos.usuario_id;
                    pres.fecha_prestamo = prestamos.fecha_prestamo;
                    pres.fecha_devolucion = prestamos.fecha_devolucion;
                    pres.estado_prestamo = prestamos.estado_prestamo;

                    conexion.Prestamos.Add(pres);
                    result = conexion.SaveChanges() > 0;
                }
            }
            return Task.FromResult(result);
        }

        public Task<List<Prestamos>> ListarPrestamos()
        {
            List<Prestamos> list = new List<Prestamos>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Prestamos
                                select c).ToList();

                foreach (var item in consulta)
                {
                    list.Add(new Prestamos()
                    {
                        prestamo_id = item.prestamo_id,
                        libro_id = item.libro_id,
                        usuario_id = item.usuario_id,
                        fecha_prestamo = item.fecha_prestamo,
                        fecha_devolucion = item.fecha_devolucion,
                        estado_prestamo = item.estado_prestamo
                        
                    });
                }

                return Task.FromResult(list);

            }
        }
    }
}
