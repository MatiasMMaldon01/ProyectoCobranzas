using IServicios.Persona;
using Microsoft.AspNetCore.Mvc;
using IServicios.Persona.DTO_s;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        public EmpleadoController(IEmpleadoServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(EmpleadoDTO empleado)
        {
            var entidad = new EmpleadoDTO
            {
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Dni = empleado.Dni,
                Direccion = empleado.Direccion,
                Telefono = empleado.Telefono,
                Mail = empleado.Mail,
                AreaTrabajo = empleado.AreaTrabajo,
                Eliminado = false,

            };

            var id = await _empleadoServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(EmpleadoDTO empleado)
        {
            var entidad = new EmpleadoDTO
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Dni = empleado.Dni,
                Direccion = empleado.Direccion,
                Telefono = empleado.Telefono,
                Mail = empleado.Mail,
                AreaTrabajo = empleado.AreaTrabajo,
                Eliminado = false,

            };

            await _empleadoServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(long id)
        {
            await _empleadoServicio.Eliminar(typeof(EmpleadoDTO),id);

            return Results.Ok("La empleado se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(long id)
        {
            var empleado = await _empleadoServicio.Obtener(typeof(EmpleadoDTO),id);

            if (empleado == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(empleado);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar, bool mostrarTodos = false )
        {
            var empleados = await _empleadoServicio.Obtener(typeof(EmpleadoDTO),cadenaBuscar, mostrarTodos);

            if (empleados == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(empleados);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var empleados = await _empleadoServicio.ObtenerTodos(typeof(EmpleadoDTO));

            if (empleados == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(empleados);
            }
        }
    }
}
