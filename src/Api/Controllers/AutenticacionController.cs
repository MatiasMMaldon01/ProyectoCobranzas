using IServicios.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/Authn")]
    public class AutenticacionController : Controller
    {
        public readonly ISeguridadServicio _seguridadServicio;
        public AutenticacionController(ISeguridadServicio seguridadServicio)
        {
            _seguridadServicio = seguridadServicio;
        }

        [HttpPost("login")]
        public async Task<IResult> ValidarUsuario(string nombreUsuario, string password)
        {
            var usuario = await _seguridadServicio.ValidarUsuario(nombreUsuario, password);

            if (usuario == null) return Results.BadRequest("Usuario no encontrado");

            var token = _seguridadServicio.CrearToken(usuario);

            return Results.Ok(token);

        }      
    }
}
