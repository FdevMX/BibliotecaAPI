using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Facturas
    {
        [Key]
        public int factura_id { get; set; }
        public int prestamo_id { get; set; }
        public DateOnly fecha_emision { get; set; }
        public decimal? monto_total { get; set; }
        public string? detalles_pago { get; set; }
        public string? estado_factura { get; set; }
    }
}
