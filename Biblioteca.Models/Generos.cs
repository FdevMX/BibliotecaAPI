using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Generos
    {
        [Key]
        public int genero_id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }

    }
}
