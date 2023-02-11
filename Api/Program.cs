using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using IServicios.Carrera;
using Servicios.CarreraServicio;
using Infraestructura.UnidadDeTrabajo;
using Dominio.Interfaces;
using Servicios.UsuarioServicio;
using IServicios.Usuario;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IServicios.Seguridad;
using Servicios.SeguridadServicio;
using IServicios.Persona;
using Servicios.PersonaServicio;
using IServicios.PrecioCuota;
using Servicios.PrecioCuotaServicio;
using IServicios.Cuota;
using Servicios.CuotaServicio;
using IServicios.Pago;
using Servicios.PagoServicio;
using IServicios.AlumnoCarrera;
using Servicios.AlumnoCarreraServicio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Inyección de Dependencias
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<ICarreraServicio, CarreraServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ISeguridadServicio, SeguridadServicio>();
builder.Services.AddScoped<IAlumnoServicio, AlumnoServicio>();
builder.Services.AddScoped<IEmpleadoServicio, EmpleadoServicio>();
builder.Services.AddScoped<IPersonaServicio, PersonaServicio>();
builder.Services.AddScoped<IPrecioCuotaServicio, PrecioCuotaServicio>();
builder.Services.AddScoped<ICuotaServicio, CuotaServicio>();
builder.Services.AddScoped<IPagoServicio, PagoServicio>();
builder.Services.AddScoped<IAlumnoCarreraServicio, AlumnoCarreraServicio>();



// DB Configuración
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("CobranzasDB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
