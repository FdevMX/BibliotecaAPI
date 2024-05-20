using Biblioteca.Helpers;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.SQLServer
{
    public class BibliotecaDataContext : DbContext  //clase heredada para hacer conexion a la bd
    {
        //Tabla de la bd donde ira los datos
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Libros> Libros { get; set; }
        public virtual DbSet<Autores> Autores { get; set; }
        public virtual DbSet<Prestamos> Prestamos { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }



        public virtual DbSet<Devoluciones> Devoluciones { get; set; }














        public virtual DbSet<Editoriales> Editoriales { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuraciones.CadenaConexion,  //ruta de cadena de conexion
                    builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); });
            }
        }
    }
}
