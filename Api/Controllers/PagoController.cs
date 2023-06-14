using Api.PersistenceModels;
using IServicios.Cuota.CuotaDTO;
using IServicios.Pago;
using IServicios.Pago.PagoDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class PagoController : Controller
    {
        private readonly IPagoServicio _pagoServicio;

        public PagoController(IPagoServicio pagoServicio)
        {
            _pagoServicio = pagoServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(PagoModel pago)
        {
            var entidad = new PagoDTO
            {
                Monto = pago.Monto,
                CuotaId = pago.CuotaId,
            };

            var id = await _pagoServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(PagoModel pago)
        {
            var entidad = new PagoDTO
            {
                Id = pago.Id,
                Monto = pago.Monto,
                CuotaId = pago.CuotaId,
                Eliminado = false,
            };

            await _pagoServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _pagoServicio.Eliminar(id);

            return Results.Ok("El Pago se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var pago = await _pagoServicio.Obtener(id);

            if (pago == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(pago);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var pago = await _pagoServicio.ObtenerTodos();

            if (pago == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(pago);
            }
        }

        [HttpGet]
        [Route("ObtenerPorAlumnoId")]
        public async Task<IResult> ObtenerPorAlumnoId(int alumnoId)
        {
            var pago = await _pagoServicio.ObtenerPorAlumnoId(alumnoId);

            if (pago == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(pago);
            }
        }

    }
}
