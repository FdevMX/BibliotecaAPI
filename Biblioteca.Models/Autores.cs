using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Autores
    {
        [Key]
        public int autor_id { get; set; }
        public string? nombre { get; set; }
        public string? apellidos { get; set; }
        public DateOnly nacimiento { get; set; }
        public string? nacionalidad { get; set; }
        public string? biografia { get; set; }

    }
}
