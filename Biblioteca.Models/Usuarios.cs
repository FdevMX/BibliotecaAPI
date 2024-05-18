using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Usuarios
    {
        // Se pone un ? (ternario) para permitir que un atributo
        // acepte valores nulos
        [Key]
        public int usuario_id { get; set; }
        public string? nombre { get; set; }
        public string? apellidos { get; set; }
        public string? direccion { get; set; }
        public string? ciudad { get; set; }
        public string? pais { get; set; }
        public string? email { get; set; }
        public string? telefono { get; set; }
        public string? rol_usuario { get; set; }
        public string? name_user { get; set; }
        public string? pasword { get; set; }

    }
}
