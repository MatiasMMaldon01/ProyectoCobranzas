using Api.PersistenceModels;
using IServicios.Cuota;
using IServicios.Cuota.CuotaDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class CuotaController : Controller
    {
        private readonly ICuotaServicio _cuotaServicio;

        public CuotaController(ICuotaServicio cuotaServicio)
        {
           _cuotaServicio = cuotaServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(CuotaModel cuota)
        {
            var entidad = new CuotaDTO
            {
                Id = cuota.Id,
                Numero = cuota.Numero,
                AlumnoId = cuota.AlumnoId,
                PrecioCuotaId = cuota.PrecioCuotaId,
                Eliminado = false,
            };

            long id = await _cuotaServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(CuotaModel cuota)
        {
            var entidad = new CuotaDTO
            {
                Id = cuota.Id,
                Numero = cuota.Numero,
                AlumnoId = cuota.AlumnoId,
                PrecioCuotaId = cuota.PrecioCuotaId,
                Eliminado = false,
            };

            await _cuotaServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(long id)
        {
            await _cuotaServicio.Eliminar(id);

            return Results.Ok("La Cuota se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(long id)
        {
            var cuota = await _cuotaServicio.Obtener(id);

            if (cuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(cuota);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var cuota = await _cuotaServicio.ObtenerTodos();

            if (cuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(cuota);
            }
        }

        [HttpGet]
        [Route("ObtenerPorAlumnoYCarrera")]
        public async Task<IResult> ObtenerPorAlumnoYCarrera(long alumnoId, long carreraId)
        {
            var cuota = await _cuotaServicio.ObtenerPorCarreraIdAlumnoId(alumnoId, carreraId);

            if (cuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(cuota);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var cuota = await _cuotaServicio.Obtener(cadenaBuscar);

            if (cuota == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(cuota);
            }
        }


    }
}
