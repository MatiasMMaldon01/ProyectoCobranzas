using IServicios.Persona;
using Microsoft.AspNetCore.Mvc;
using IServicios.Persona.DTO_s;
using Microsoft.AspNetCore.Authorization;
using IServicios.Contador;
using Aplicacion.Constantes.Enums;
using Dominio.Entidades;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IContadorServicio _contadorServicio;
        public EmpleadoController(IEmpleadoServicio empleadoServicio, IContadorServicio contadorServicio)
        {
            _empleadoServicio = empleadoServicio;
            _contadorServicio = contadorServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(EmpleadoDTO empleado)
        {
            var entidad = new EmpleadoDTO
            {
                Apynom = empleado.Apynom,
                TipoDoc = empleado.TipoDoc,
                NroDoc = empleado.NroDoc,
                FechaNacimiento = empleado.FechaNacimiento,
                Direccion = empleado.Direccion,
                Telefono = empleado.Telefono,
                Mail = empleado.Mail,
                AreaTrabajo = empleado.AreaTrabajo,
                CiudadId = empleado.CiudadId,
                ExtensionId = empleado.ExtensionId,
                Eliminado = false,
            };
            var id = await _empleadoServicio.Crear(entidad);

            await _contadorServicio.CargarNumero(Entidad.Persona, id);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(EmpleadoDTO empleado)
        {
            var entidad = new EmpleadoDTO
            {
                Id = empleado.Id,
                Apynom = empleado.Apynom,
                TipoDoc = empleado.TipoDoc,
                NroDoc = empleado.NroDoc,
                FechaNacimiento = empleado.FechaNacimiento,
                Direccion = empleado.Direccion,
                Telefono = empleado.Telefono,
                Mail = empleado.Mail,
                AreaTrabajo = empleado.AreaTrabajo,
                CiudadId = empleado.CiudadId,
                ExtensionId = empleado.ExtensionId,
                Eliminado = false,
            };

            await _empleadoServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _empleadoServicio.Eliminar(typeof(EmpleadoDTO),id);

            return Results.Ok("La empleado se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var empleado = await _empleadoServicio.Obtener(typeof(EmpleadoDTO),id);

            if (empleado.Eliminado)
            {
                return Results.BadRequest("El empleado que está buscando fue eliminado");
            }

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
