using IServicios.Persona;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiltrosController : Controller
    {
        private readonly IAlumnoServicio _alumnoServicio;

        public FiltrosController(IAlumnoServicio alumnoServicio)
        {
            _alumnoServicio = alumnoServicio;
        }

        [HttpGet]
        public async Task<IResult> FiltroAlumnos(DateTime fechaDeCorte)
        {
            int cuotasDeCorte = fechaDeCorte.Month - 2;

            var alumnos = await _alumnoServicio.FiltrarAlumnos(cuotasDeCorte);
            return Results.Ok(alumnos);
        }
    }
}
