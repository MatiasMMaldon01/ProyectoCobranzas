using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Extension.DTO_s;
using IServicios.Extension;
using System.Linq.Expressions;
using Servicios.Base;

namespace Servicios.ExtensionServicio
{
    public class ExtensionServicio : IExtensionServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ExtensionServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {

            var dto = (ExtensionDTO)dtoEntidad;

            var entidad = new Extension
            {
                Descripcion = dto.Descripcion,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.ExtensionRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {

            var dto = (ExtensionDTO)dtoEntidad;

            var entidad = await _unidadDeTrabajo.ExtensionRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró la extension que quiere modificar");

            entidad.Descripcion = dto.Descripcion;

            await _unidadDeTrabajo.ExtensionRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.ExtensionRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.ExtensionRepositorio.Obtener(id);

            if (entidad == null) throw new Exception("No se encotró la extension que esta buscando");

            return new ExtensionDTO
            {
                Id = entidad.Id,
                Descripcion = entidad.Descripcion,
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<Extension, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var entidad = await _unidadDeTrabajo.ExtensionRepositorio.ObtenerTodos(filtro);

            return entidad.Select(x => new ExtensionDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<Extension, bool>> filtro = x => x.Descripcion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.ExtensionRepositorio.Obtener(filtro);

            return entidad.Select(x => new ExtensionDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }
    }
}
