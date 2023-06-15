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
        public async Task<IResult> Crear(ExtensionModel Extension)
        {
            var entidad = new ExtensionDTO
            {
                Descripcion = Extension.Descripcion,
                Eliminado = false,
            };

            long id = await _extensionServicio.Crear(entidad);

            return Results.Ok(id);

        }

        [HttpPut]
        public async Task<IResult> Modificar(ExtensionModel Extension)
        {
            var entidad = new ExtensionDTO
            {
                Id = Extension.Id,
                Descripcion = Extension.Descripcion,
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
            var Extension = await _extensionServicio.Obtener(id);

            if (Extension == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Extension);
            }
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<IResult> ObtenerTodos()
        {
            var Extensions = await _extensionServicio.ObtenerTodos();

            if (Extensions == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Extensions);
            }
        }

        [HttpGet]
        public async Task<IResult> Obtener(string? cadenaBuscar)
        {
            var Extensions = await _extensionServicio.Obtener(cadenaBuscar);

            if (Extensions == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(Extensions);
            }
        }
    }
}
