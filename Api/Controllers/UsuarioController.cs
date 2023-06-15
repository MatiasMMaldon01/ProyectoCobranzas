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
        private readonly IUsuarioServicio _UsuarioServicio;

        public UsuarioController(IUsuarioServicio UsuarioServicio)
        {
            _UsuarioServicio = UsuarioServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(UsuarioModel Usuario)
        {
            var entidad = new UsuarioDTO
            {
                Nombre = Usuario.Nombre,
                Password = Usuario.Password,
                Rol = Usuario.Rol,
                PersonaId = Usuario.PersonaId,
                Eliminado = false,

            };

            int id = await _UsuarioServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(UsuarioModel Usuario)
        {
            var entidad = new UsuarioDTO
            {
                Id = Usuario.Id,
                Nombre = Usuario.Nombre,
                Password = Usuario.Password,
                Rol = Usuario.Rol,
                PersonaId=Usuario.PersonaId,
                Eliminado = false,

            };

            await _UsuarioServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _UsuarioServicio.Eliminar(id);

            return Results.Ok("La Usuario se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
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
        public async Task<IResult> Obtener(string? cadenaBuscar)
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
