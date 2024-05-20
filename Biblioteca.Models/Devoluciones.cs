using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Devoluciones
    {
        [Key]
        public int devolucion_id { get; set; }
        public int prestamo_id { get; set; }
        public DateOnly fecha_devolucion { get; set; }      
        public string? estado_libro { get; set; }
        public string? observaciones { get; set; }

    }
}
