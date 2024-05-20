using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Editoriales
    {
        [Key]
        public int editorial_id { get; set; }
        public string? nombre { get; set; }
        public string? direccion { get; set; }
        public string? ciudad { get; set; }
        public string? pais { get; set; }
        public string? descripcion { get; set; }
    }
}
