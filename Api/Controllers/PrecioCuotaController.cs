﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IServicios.PrecioCuota;
using IServicios.PrecioCuota.PrecioCuotaDTO;
using Api.PersistenceModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class PrecioCuotaController : Controller
    {
        private readonly IPrecioCuotaServicio _precioCuotaServicio;

        public PrecioCuotaController(IPrecioCuotaServicio precioCuotaServicio)
        {
            _precioCuotaServicio = precioCuotaServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(PrecioCuotaModel precioCuota)
        {
            var entidad = new PrecioCuotaDTO
            {
                Id = precioCuota.Id,
                Monto = precioCuota.Monto,
                Fecha = precioCuota.Fecha,
                CarreraId = precioCuota.CarreraId,
                Eliminado = false,

            };

            long id = await _precioCuotaServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(PrecioCuotaModel precioCuota)
        {
            var entidad = new PrecioCuotaDTO
            {
                Id = precioCuota.Id,
                Monto = precioCuota.Monto,
                Fecha = precioCuota.Fecha,
                CarreraId = precioCuota.CarreraId,
                Eliminado = false,
            };

            await _precioCuotaServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _precioCuotaServicio.Eliminar(id);

            return Results.Ok("El Precio se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var precioCuota = await _precioCuotaServicio.Obtener(id);

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
            var precioCuota = await _precioCuotaServicio.ObtenerTodos();

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
            var precioCuota = await _precioCuotaServicio.ObtenerPorCarreraId(carreraId);

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
            var precioCuota = await _precioCuotaServicio.Obtener(cadenaBuscar);

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
