using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IServicios.PrecioCuota;
using IServicios.PrecioCuota.PrecioCuotaDTO;
using Api.PersistenceModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class PrecioCarreraController : Controller
    {
        private readonly IPrecioCarreraServicio _precioCarreraServicio;

        public PrecioCarreraController(IPrecioCarreraServicio precioCarreraServicio)
        {
            _precioCarreraServicio = precioCarreraServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(PrecioCarreraModel precioCarrera)
        {
            var entidad = new PrecioCarreraDTO
            {
                Id = precioCarrera.Id,
                Monto = precioCarrera.Monto,
                Fecha = precioCarrera.Fecha,
                Matricula = precioCarrera.Matricula,
                CarreraId = precioCarrera.CarreraId,
                Eliminado = false,

            };

            long id = await _precioCarreraServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(PrecioCarreraModel precioCarrera)
        {
            var entidad = new PrecioCarreraDTO
            {
                Id = precioCarrera.Id,
                Monto = precioCarrera.Monto,
                Fecha = precioCarrera.Fecha,
                Matricula = precioCarrera.Matricula,
                CarreraId = precioCarrera.CarreraId,
                Eliminado = false,
            };

            await _precioCarreraServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _precioCarreraServicio.Eliminar(id);

            return Results.Ok("El Precio se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var precioCuota = await _precioCarreraServicio.Obtener(id);

            if (precioCuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(precioCuota);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var precioCuota = await _precioCarreraServicio.ObtenerTodos();

            if (precioCuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(precioCuota);
            }
        }

        [HttpGet]
        [Route("ObtenerPorCarreraId")]
        public async Task<IResult> ObtenerPorCarreraId(int carreraId)
        {
            var precioCuota = await _precioCarreraServicio.ObtenerPorCarreraId(carreraId);

            if (precioCuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(precioCuota);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var precioCuota = await _precioCarreraServicio.Obtener(cadenaBuscar);

            if (precioCuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(precioCuota);
            }
        }

    }
}
