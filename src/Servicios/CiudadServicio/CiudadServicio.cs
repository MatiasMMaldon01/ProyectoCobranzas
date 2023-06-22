using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Carrera.Carrera_DTO;
using IServicios.Ciudad;
using IServicios.Ciudad.DTO_s;
using Servicios.Base;
using System.Linq.Expressions;

namespace Servicios.CiudadServicio
{
    public class CiudadServicio : ICiudadServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CiudadServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;         
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {

            var dto = (CiudadDTO)dtoEntidad;

            var entidad = new Ciudad
            {
                Descripcion = dto.Descripcion,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.CiudadRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {

            var dto = (CiudadDTO)dtoEntidad;

            var entidad = await _unidadDeTrabajo.CiudadRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró la ciudad que quiere modificar");

            entidad.Descripcion = dto.Descripcion;

            await _unidadDeTrabajo.CiudadRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.CiudadRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }
       
        public async Task<BaseDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.CiudadRepositorio.Obtener(id);

            if (entidad == null) throw new Exception("No se encotró la ciudad que esta buscando");

            return new CiudadDTO
            {
                Id = entidad.Id,
                Descripcion = entidad.Descripcion,
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<Ciudad, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var entidad = await _unidadDeTrabajo.CiudadRepositorio.ObtenerTodos(filtro);

            return entidad.Select(x => new CiudadDTO
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
            Expression<Func<Ciudad, bool>> filtro = x => x.Descripcion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.CiudadRepositorio.Obtener(filtro);

            return entidad.Select(x => new CiudadDTO
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
