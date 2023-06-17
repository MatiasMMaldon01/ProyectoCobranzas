using Api.PersistenceModels;
using IServicios.Ciudad;
using Microsoft.AspNetCore.Mvc;
using IServicios.Ciudad.DTO_s;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadController : Controller
    {
        private readonly ICiudadServicio _ciudadServicio;

        public CiudadController(ICiudadServicio ciudadServicio)
        {
            _ciudadServicio = ciudadServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(CiudadModel Ciudad)
        {
            var entidad = new CiudadDTO
            {
                Descripcion = Ciudad.Descripcion,
                Eliminado = false,
            };

            int id = await _ciudadServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(CiudadModel Ciudad)
        {
            var entidad = new CiudadDTO
            {
                Id = Ciudad.Id,
                Descripcion = Ciudad.Descripcion,
                Eliminado = false,

            };

            await _ciudadServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _ciudadServicio.Eliminar(id);

            return Results.Ok("La Ciudad se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var Ciudad = await _ciudadServicio.Obtener(id);

            if (Ciudad == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Ciudad);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var Ciudads = await _ciudadServicio.ObtenerTodos();

            if (Ciudads == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Ciudads);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var Ciudads = await _ciudadServicio.Obtener(cadenaBuscar);

            if (Ciudads == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Ciudads);
            }
        }
    }
}
