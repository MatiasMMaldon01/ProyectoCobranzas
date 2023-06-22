using Api.PersistenceModels;
using IServicios.Extension.DTO_s;
using IServicios.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtensionController : Controller
    {
        private readonly IExtensionServicio _extensionServicio;

        public ExtensionController(IExtensionServicio extensionServicio)
        {
            _extensionServicio = extensionServicio;
        }

        [HttpPost]
        public async Task<IResult> Crear(ExtensionModel extension)
        {
            var entidad = new ExtensionDTO
            {
                Descripcion = extension.Descripcion,
                Eliminado = false,
            };

            int id = await _extensionServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(ExtensionModel extension)
        {
            var entidad = new ExtensionDTO
            {
                Id = extension.Id,
                Descripcion = extension.Descripcion,
                Eliminado = false,

            };

            await _extensionServicio.Modificar(entidad);

            return Results.Ok(entidad);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Eliminar(int id)
        {
            await _extensionServicio.Eliminar(id);

            return Results.Ok("La Extension se eliminó correctamente");
        }

        [HttpGet("{id}")]
        public async Task<IResult> Obtener(int id)
        {
            var extension = await _extensionServicio.Obtener(id);

            if (extension.Eliminado)
            {
                return Results.BadRequest("La extension que está buscando fue eliminada");
            }

            if (extension == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(extension);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var extensiones = await _extensionServicio.ObtenerTodos();

            if (extensiones == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(extensiones);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var extensiones = await _extensionServicio.Obtener(cadenaBuscar);

            if (extensiones == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(extensiones);
            }
        }
    }
}
