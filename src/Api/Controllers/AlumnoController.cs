﻿using IServicios.Persona.DTO_s;
using IServicios.Persona;
using Microsoft.AspNetCore.Mvc;
using Api.PersistenceModels;
using IServicios.Persona.CargasMasivas;
using IServicios.Contador;
using Aplicacion.Constantes.Enums;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class AlumnoController : Controller
    {
        private readonly IAlumnoServicio _alumnoServicio;
        private readonly IAlumnoCargaMasiva _alumnoCargaMasiva;
        private readonly IContadorServicio _contadorServicio;

        public AlumnoController(IAlumnoServicio alumnoServicio, IAlumnoCargaMasiva alumnoCargaMasiva, IContadorServicio contadorServicio)
        {
            _alumnoServicio = alumnoServicio;
            _alumnoCargaMasiva = alumnoCargaMasiva;
            _contadorServicio = contadorServicio;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> Crear(AlumnoModel alumno)
        {
            var entidad = new AlumnoDTO
            {
                Legajo = alumno.Legajo,
                Apynom = alumno.Apynom,
                TipoDoc = alumno.TipoDoc,
                NroDoc = alumno.NroDoc,
                FechaNacimiento = alumno.FechaNacimiento,
                Direccion = alumno.Direccion,
                Telefono = alumno.Telefono,
                Mail = alumno.Mail,
                FechaIngreso = alumno.FechaIngreso,
                CiudadId = alumno.CiudadId,
                ExtensionId = alumno.ExtensionId,
                CarreraId = alumno.CarreraId,
                CodigoPostal = alumno.CodigoPostal,
                Eliminado = false,
            };

            var id = await _alumnoServicio.Crear(entidad);

            await _contadorServicio.CargarNumero(Entidad.Persona, id);

            return Results.Ok(id);

        }

        [HttpPost]
        [Route("CargaMasiva")]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> CargaMasiva()
        {
            await _alumnoCargaMasiva.CargaMasivaAlumno();

            return Results.Ok("La carga masiva de alumnos se realizó con éxito");

        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> Modificar(AlumnoModel alumno)
        {
            var entidad = new AlumnoDTO
            {
                Id = alumno.Id,
                Legajo = alumno.Legajo,
                Apynom = alumno.Apynom,
                TipoDoc = alumno.TipoDoc,
                NroDoc = alumno.NroDoc,
                FechaNacimiento = alumno.FechaNacimiento,
                Direccion = alumno.Direccion,
                Telefono = alumno.Telefono,
                Mail = alumno.Mail,
                FechaIngreso = alumno.FechaIngreso,
                CiudadId = alumno.CiudadId,
                ExtensionId = alumno.ExtensionId,
                CarreraId = alumno.CarreraId,
                CodigoPostal = alumno.CodigoPostal,
                Eliminado = false,
            };

            await _alumnoServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> Eliminar(int id)
        {
            await _alumnoServicio.Eliminar(typeof(AlumnoDTO), id);

            return Results.Ok("La Alumno se eliminó correctamente");
        }

        [HttpDelete]
        [Route("EliminarMasivo")]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> EliminarMasivo(DateTime desde, DateTime hasta)
        {
            await _alumnoCargaMasiva.EliminacionMasivaAlumnos(desde, hasta);

            return Results.Ok("La eliminación masiva de alumnos se realizó con éxito");

        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IResult> Obtener(int id)
        {
            var alumno = await _alumnoServicio.Obtener(typeof(AlumnoDTO), id);

            if (alumno.Eliminado)
            {
                return Results.BadRequest("El alumno que está buscando fue eliminado");
            }

            if (alumno == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(alumno);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, Directivo")]
        public async Task<IResult> Obtener(string? cadenaBuscar, bool mostrarTodos = false)
        {
            var alumnos = await _alumnoServicio.Obtener(typeof(AlumnoDTO), cadenaBuscar, mostrarTodos);

            if (alumnos == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(alumnos);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        //[Authorize(Roles = "Admin, Directivo")]
        public async Task<IResult> ObtenerTodos()
        {
            var alumnos = await _alumnoServicio.ObtenerTodos(typeof(AlumnoDTO));

            if (alumnos == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(alumnos);
            }
        }

        // ==================================== METODOS PRIVADOS ==================================== //

    }
}
