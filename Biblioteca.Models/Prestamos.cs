using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Prestamos
    {
        [Key]
        public int prestamo_id { get; set; }
        public int libro_id { get; set;}
        public int usuario_id { get; set; }
        public DateTime fecha_prestamo { get; set; }
        public DateOnly fecha_devolucion { get; set; }
        public string? estado_prestamo { get; set; }
    }
}
