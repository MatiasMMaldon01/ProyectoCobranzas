using Api.PersistenceModels;
using IServicios.Ciudad;
using Microsoft.AspNetCore.Mvc;
using IServicios.Ciudad.DTO_s;
using Dominio.Entidades;
using Dominio.Metadata;

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
        public async Task<IResult> Crear(CiudadModel ciudad)
        {
            var entidad = new CiudadDTO
            {
                Descripcion = ciudad.Descripcion,
                Eliminado = false,
            };

            int id = await _ciudadServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(CiudadModel ciudad)
        {
            var entidad = new CiudadDTO
            {
                Id = ciudad.Id,
                Descripcion = ciudad.Descripcion,
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
            var ciudad = await _ciudadServicio.Obtener(id);

            if (ciudad.Eliminado)
            {
                return Results.BadRequest("La ciudad que está buscando fue eliminada");
            }


            if (ciudad == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(ciudad);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var ciudades = await _ciudadServicio.ObtenerTodos();

            if (ciudades == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(ciudades);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var ciudades = await _ciudadServicio.Obtener(cadenaBuscar);

            if (ciudades == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(ciudades);
            }
        }
    }
}
