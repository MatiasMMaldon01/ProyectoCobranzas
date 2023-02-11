using Microsoft.AspNetCore.Mvc;
using IServicios.AlumnoCarrera;
using Microsoft.AspNetCore.Authorization;
using IServicios.AlumnoCarrera.AlumnoCarreraDTO;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AlumnoCarreraController : Controller
    {
        private readonly IAlumnoCarreraServicio _alumnoCarreraServicio;

        public AlumnoCarreraController(IAlumnoCarreraServicio alumnoCarreraServicio)
        {
            _alumnoCarreraServicio = alumnoCarreraServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(AlumnoCarreraDTO alumnoCarrera)
        {
            var entidad = new AlumnoCarreraDTO
            {
                CarreraId = alumnoCarrera.CarreraId,
                AlumnoId = alumnoCarrera.AlumnoId,
                Eliminado = false,

            };

            var respuesta = await _alumnoCarreraServicio.Crear(entidad);

            return Results.Ok(respuesta);

        }

        [HttpPut]
        public async Task<IResult> Modificar(AlumnoCarreraDTO alumnoCarrera)
        {
            var entidad = new AlumnoCarreraDTO
            {
                Id = alumnoCarrera.Id,
                CarreraId = alumnoCarrera.CarreraId,
                AlumnoId = alumnoCarrera.AlumnoId,
                Eliminado = false,
            };

            await _alumnoCarreraServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(long id)
        {
            await _alumnoCarreraServicio.Eliminar(id);

            return Results.Ok("La relación Alumno-Carrera se eliminó correctamente");
        }

    }
}
