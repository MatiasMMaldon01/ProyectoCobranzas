using IServicios.Carrera;
using IServicios.Carrera.Carrera_DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CarreraController : Controller
    {
        private readonly ICarreraServicio _carreraServicio;

        public CarreraController(ICarreraServicio carreraServicio)
        {
            _carreraServicio = carreraServicio;
        }

        [HttpPost]
        public async  Task<IResult> Crear(CarreraDto carrera)
        {
            var entidad = new CarreraDto
            {
                Descripcion = carrera.Descripcion,
                CantidadCuotas = carrera.CantidadCuotas,
                Eliminado = false,

            };

            await _carreraServicio.Crear(carrera);

            return Results.Ok(carrera);

        }

        [HttpPut]
        public async Task<IResult> Modificar(CarreraDto carrera)
        {
            var entidad = new CarreraDto
            {
                Descripcion = carrera.Descripcion,
                CantidadCuotas = carrera.CantidadCuotas,
                Eliminado = false,

            };

            await _carreraServicio.Modificar(carrera);

            return Results.Ok(carrera);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(long id)
        {
            await _carreraServicio.Eliminar(id);

            return Results.Ok("La Carrera se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(long id)
        {
            var carrera =  await _carreraServicio.Obtener(id);

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
        public async Task<IResult> Obtener(string cadenaBuscar)
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
