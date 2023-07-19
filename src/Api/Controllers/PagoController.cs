using Api.PersistenceModels;
using Aplicacion.Constantes.Enums;
using IServicios.Contador;
using IServicios.Pago;
using IServicios.Pago.CargasMasivas;
using IServicios.Pago.PagoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class PagoController : Controller
    {
        private readonly IPagoServicio _pagoServicio;
        private readonly IPagoCargaMasiva _pagoCargaMasiva;
        private readonly IContadorServicio _contadorServicio;

        public PagoController(IPagoServicio pagoServicio, IPagoCargaMasiva pagoCargaMasiva, IContadorServicio contadorServicio)
        {
            _pagoServicio = pagoServicio;
            _pagoCargaMasiva = pagoCargaMasiva;
            _contadorServicio = contadorServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(PagoModel pago)
        {
            var entidad = new PagoDTO
            {
                Legajo = pago.Legajo,
                CantCuota = pago.CantCuota,
                Monto = pago.Monto,
                NroRecibo = pago.NroRecibo,
                FechaCarga = pago.FechaCarga,
                FechaRecibo = pago.FechaRecibo,
            };

            var id = await _pagoServicio.Crear(entidad);

            await _contadorServicio.CargarNumero(Entidad.Pago, id);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(PagoModel pago)
        {
            var entidad = new PagoDTO
            {
                Id = pago.Id,
                Legajo = pago.Legajo,
                Monto = pago.Monto,
                NroRecibo = pago.NroRecibo,
            };

            await _pagoServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpPost]
        [Route("CargaMasiva")]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> CargaMasiva()
        {
            await _pagoCargaMasiva.CargaMasivaPago();

            return Results.Ok("La carga masiva de pagos se realizó con éxito");

        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _pagoServicio.Eliminar(id);

            return Results.Ok("El Pago se eliminó correctamente");
        }

        [HttpDelete]
        [Route("EliminarMasivo")]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> EliminarMasivo(DateTime desde, DateTime hasta)
        {
            await _pagoCargaMasiva.EliminacionMasivaPagos(desde, hasta);

            return Results.Ok("La eliminación masiva de pagos se realizó con éxito");

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
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var pagos = await _pagoServicio.Obtener(cadenaBuscar);

            if (pagos == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(pagos);
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
