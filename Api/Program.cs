using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using IServicios.Carrera;
using Servicios.CarreraServicio;
using Infraestructura.UnidadDeTrabajo;
using Dominio.Interfaces;
using Servicios.UsuarioServicio;
using IServicios.Usuario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Inyección de Dependencias
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<ICarreraServicio, CarreraServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();



builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CobranzasDB")));

var app = builder.Build();

// Esta porción de código crea la DB. Sólo usar una vez. (Reescribe la DB)
//using (var scope = app.Services.CreateScope())
//{

//    var _context = scope.ServiceProvider.GetRequiredService<DataContext>();
//    _context.Database.Migrate();

//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
