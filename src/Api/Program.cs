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
using IServicios.Seguridad;
using Servicios.SeguridadServicio;
using IServicios.Persona;
using Servicios.PersonaServicio;
using IServicios.Pago;
using Servicios.PagoServicio;
using IServicios.PrecioCuota;
using Servicios.PrecioCuotaServicio;
using IServicios.Extension;
using Servicios.ExtensionServicio;
using IServicios.Ciudad;
using Servicios.CiudadServicio;
using IServicios.Persona.CargasMasivas;
using Servicios.PersonaServicio.AlumnoCMServicio;
using Servicios.Contador;
using IServicios.Contador;
using IServicios.Pago.CargasMasivas;
using Servicios.PagoServicio.PagoCMServicio;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

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
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });


// Inyecci�n de Dependencias
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<ICarreraServicio, CarreraServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ISeguridadServicio, SeguridadServicio>();
builder.Services.AddScoped<IAlumnoServicio, AlumnoServicio>();
builder.Services.AddScoped<IEmpleadoServicio, EmpleadoServicio>();
builder.Services.AddScoped<IPersonaServicio, PersonaServicio>();
builder.Services.AddScoped<IPrecioCarreraServicio, PrecioCarreraServicio>();
builder.Services.AddScoped<IPagoServicio, PagoServicio>();
builder.Services.AddScoped<IExtensionServicio, ExtensionServicio>();
builder.Services.AddScoped<ICiudadServicio, CiudadServicio>();
builder.Services.AddScoped<IContadorServicio, ContadorServicio>();
builder.Services.AddScoped<IAlumnoCargaMasiva, AlumnoCMServicio>();
builder.Services.AddScoped<IPagoCargaMasiva, PagoCMServicio>();





// DB Configuraci�n
builder.Services.AddDbContext<DataContext>(options => {
     options.UseSqlServer(builder.Configuration.GetConnectionString("TesisDB"));
     options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
