using Api.PersistenceModels;
using IServicios.Carrera;
using IServicios.Carrera.Carrera_DTO;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class CarreraController : Controller
    {
        private readonly ICarreraServicio _carreraServicio;

        public CarreraController(ICarreraServicio carreraServicio)
        {
            _carreraServicio = carreraServicio;
        }

        [HttpPost]
        public async  Task<IResult> Crear(CarreraModel carrera)
        {
            var entidad = new CarreraDto
            {
                Descripcion = carrera.Descripcion,
                CantCuotas = carrera.CantCuotas,
                Eliminado = false,
            };

            int id = await _carreraServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(CarreraModel carrera)
        {
            var entidad = new CarreraDto
            {
                Id = carrera.Id,
                Descripcion = carrera.Descripcion,
                CantCuotas = carrera.CantCuotas,
                Eliminado = false,

            };

            await _carreraServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _carreraServicio.Eliminar(id);

            return Results.Ok("La Carrera se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var carrera =  await _carreraServicio.Obtener(id);

            if (carrera.Eliminado)
            {
                return Results.BadRequest("La carrera que está buscando fue eliminada");
            }

            if(carrera == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(carrera);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var carreras = await _carreraServicio.ObtenerTodos();

            if (carreras == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(carreras);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var carreras = await _carreraServicio.Obtener(cadenaBuscar);

            if (carreras == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(carreras);
            }
        }
      

    }
}
