using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Libros
    {
        [Key]
        public int libro_id { get; set; }
        public string? titulo { get; set; }
        public string? isbn { get; set; }
        public string? descripcion { get; set; }
        public DateOnly anio_pub { get; set; }
        public int num_paginas { get; set; }
        public string? idioma { get; set; }
        public string? estado { get; set; }
        public int autor_id { get; set; }
        public int editorial_id { get; set; }
        public int genero_id { get; set; }
        public int copy_dispo { get; set; }

    }
}
