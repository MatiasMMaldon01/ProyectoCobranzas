using IServicios.Persona.DTO_s;
using IServicios.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AlumnoController : Controller
    {
        private readonly IAlumnoServicio _alumnoServicio;
        public AlumnoController(IAlumnoServicio alumnoServicio)
        {
            _alumnoServicio = alumnoServicio;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> Crear(AlumnoDTO alumno)
        {
            var entidad = new AlumnoDTO
            {
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                Dni = alumno.Dni,
                Direccion = alumno.Direccion,
                Telefono = alumno.Telefono,
                Mail = alumno.Mail,
                FechaIngreso = alumno.FechaIngreso,
                CarreraId = alumno.CarreraId,
                Legajo = alumno.Legajo,
                Eliminado = false,

            };

            var id = await _alumnoServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> Modificar(AlumnoDTO alumno)
        {
            var entidad = new AlumnoDTO
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                Dni = alumno.Dni,
                Direccion = alumno.Direccion,
                Telefono = alumno.Telefono,
                Mail = alumno.Mail,
                FechaIngreso = alumno.FechaIngreso,
                CarreraId = alumno.CarreraId,
                Legajo = alumno.Legajo,
                Eliminado = false,

            };

            await _alumnoServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> Eliminar(long id)
        {
            await _alumnoServicio.Eliminar(typeof(AlumnoDTO), id);

            return Results.Ok("La Alumno se eliminó correctamente");
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> Obtener(long id)
        {
            var Alumno = await _alumnoServicio.Obtener(typeof(AlumnoDTO), id);

            if (Alumno == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Alumno);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Directivo")]
        public async Task<IResult> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            var Alumnos = await _alumnoServicio.Obtener(typeof(AlumnoDTO), cadenaBuscar, mostrarTodos);

            if (Alumnos == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Alumnos);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        [Authorize(Roles = "Admin, Directivo")]
        public async Task<IResult> ObtenerTodos()
        {
            var Alumnos = await _alumnoServicio.ObtenerTodos(typeof(AlumnoDTO));

            if (Alumnos == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Alumnos);
            }
        }
    }
}
