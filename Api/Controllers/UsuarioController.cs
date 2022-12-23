using IServicios.Usuario;
using Microsoft.AspNetCore.Mvc;
using IServicios.Usuario.UsuarioDTO;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicio _UsuarioServicio;

        public UsuarioController(IUsuarioServicio UsuarioServicio)
        {
            _UsuarioServicio = UsuarioServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(UsuarioDTO Usuario)
        {
            var entidad = new UsuarioDTO
            {
                Nombre = Usuario.Nombre,
                Password = Usuario.Password,
                Rol = Usuario.Rol,
                Eliminado = false,

            };

            await _UsuarioServicio.Crear(Usuario);

            return Results.Ok(Usuario);

        }

        [HttpPut]
        public async Task<IResult> Modificar(UsuarioDTO Usuario)
        {
            var entidad = new UsuarioDTO
            {
                Nombre = Usuario.Nombre,
                Password = Usuario.Password,
                Rol = Usuario.Rol,
                Eliminado = false,

            };

            await _UsuarioServicio.Modificar(Usuario);

            return Results.Ok(Usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(long id)
        {
            await _UsuarioServicio.Eliminar(id);

            return Results.Ok("La Usuario se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(long id)
        {
            var Usuario = await _UsuarioServicio.Obtener(id);

            if (Usuario == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Usuario);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var Usuarios = await _UsuarioServicio.ObtenerTodos();

            if (Usuarios == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Usuarios);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string cadenaBuscar)
        {
            var Usuarios = await _UsuarioServicio.Obtener(cadenaBuscar);

            if (Usuarios == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Usuarios);
            }
        }
    }


}
