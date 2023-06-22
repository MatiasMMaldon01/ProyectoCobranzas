using IServicios.Persona;
using IServicios.Persona.DTO_s;
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
            int mes = fechaDeCorte.Month;

            return Results.Ok(mes);
        }
    }
}
