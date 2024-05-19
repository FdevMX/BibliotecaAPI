using Biblioteca.Core.BL.Interfaces;
using Biblioteca.Core.BL.Services;
using Biblioteca.Helpers;

var builder = WebApplication.CreateBuilder(args);

//Obtener la cadena de conexion
Configuraciones.CadenaConexion = builder.Configuration.GetConnectionString("CadenaConexion");


// Add services to the container.
builder.Services.AddControllers();

//Agregar inyeccion de dependencias
builder.Services.AddTransient<ISeguridad, SeguridadService>();
builder.Services.AddTransient<IUsuarios, UsuariosService>();
builder.Services.AddTransient<IAutores, AutoresService>();

//Activar Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Validar que se ejecute swagger solo en modo programadorç
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
