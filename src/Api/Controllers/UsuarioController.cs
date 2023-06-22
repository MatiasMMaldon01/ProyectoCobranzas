using IServicios.Usuario;
using Microsoft.AspNetCore.Mvc;
using IServicios.Usuario.UsuarioDTO;
using Api.PersistenceModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio UsuarioServicio)
        {
            _usuarioServicio = UsuarioServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(UsuarioModel usuario)
        {
            var entidad = new UsuarioDTO
            {
                Nombre = usuario.Nombre,
                Password = usuario.Password,
                Rol = usuario.Rol,
                PersonaId = usuario.PersonaId,
                Eliminado = false,

            };

            int id = await _usuarioServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(UsuarioModel usuario)
        {
            var entidad = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Password = usuario.Password,
                Rol = usuario.Rol,
                PersonaId= usuario.PersonaId,
                Eliminado = false,

            };

            await _usuarioServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _usuarioServicio.Eliminar(id);

            return Results.Ok("La Usuario se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var usuario = await _usuarioServicio.Obtener(id);

            if (usuario.Eliminado)
            {
                return Results.BadRequest("El usuario que está buscando fue eliminado");
            }

            if (usuario == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(usuario);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var usuarios = await _usuarioServicio.ObtenerTodos();

            if (usuarios == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(usuarios);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var usuarios = await _usuarioServicio.Obtener(cadenaBuscar);

            if (usuarios == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(usuarios);
            }
        }
    }

}
