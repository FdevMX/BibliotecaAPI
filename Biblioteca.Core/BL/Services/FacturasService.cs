using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.BL.Services
{
    public class FacturasService : IFacturas
    {
        public Task<bool> ActualizarFactura(Facturas facturas)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Facturas
                                where c.factura_id == facturas.factura_id
                                select c).FirstOrDefault();
                if (consulta != null)
                {
                    consulta.prestamo_id = facturas.prestamo_id;
                    consulta.fecha_emision = facturas.fecha_emision;
                    consulta.monto_total = facturas.monto_total;
                    consulta.detalles_pago = facturas.detalles_pago;
                    consulta.estado_factura = facturas.estado_factura;

                    result = conexion.SaveChanges() > 0;

                }

            }
            return Task.FromResult(result);
        }

        public async Task<bool> EliminarFactura(int facturaId)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = conexion.Facturas.FirstOrDefault(c => c.factura_id == facturaId);
                if (consulta != null)
                {
                    conexion.Facturas.Remove(consulta);
                    result = await conexion.SaveChangesAsync() > 0;
                }
            }
            return result;

        }

        public Task<bool> GuardarFactura(Facturas facturas)
        {
            bool result = false;
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Facturas
                                where c.factura_id == facturas.factura_id
                                select c).FirstOrDefault();

                if (consulta == null)
                {
                    Facturas fac = new Facturas();

                    fac.prestamo_id = facturas.prestamo_id;
                    fac.fecha_emision = facturas.fecha_emision;
                    fac.monto_total = facturas.monto_total;
                    fac.detalles_pago = facturas.detalles_pago;
                    fac.estado_factura = facturas.estado_factura;

                    conexion.Facturas.Add(fac);
                    result = conexion.SaveChanges() > 0;

                }

            }
            return Task.FromResult(result);
        }

        public Task<List<Facturas>> ListarFacturas()
        {
            List<Facturas> list = new List<Facturas>();
            using (var conexion = new Data.SQLServer.BibliotecaDataContext())
            {
                var consulta = (from c in conexion.Facturas
                                select c).ToList();
                foreach (var item in consulta)
                {
                    list.Add(new Facturas()
                    {
                        factura_id = item.factura_id,
                        prestamo_id = item.prestamo_id,
                        fecha_emision = item.fecha_emision,
                        monto_total = item.monto_total,
                        detalles_pago = item.detalles_pago,
                        estado_factura = item.estado_factura
                    });

                }
                return Task.FromResult(list);
            }
        }
    }
}
