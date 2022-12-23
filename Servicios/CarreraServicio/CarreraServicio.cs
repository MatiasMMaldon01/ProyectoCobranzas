using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Carrera;
using IServicios.Carrera.Carrera_DTO;
using IServicios.Usuario.UsuarioDTO;
using Servicios.Base;
using System.Linq.Expressions;

namespace Servicios.CarreraServicio
{
    public class CarreraServicio : ICarreraServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public CarreraServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
           _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Eliminar(long id)
        {
            await _unidadDeTrabajo.CarreraRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<long> Crear(BaseDTO dtoEntidad)
        {

            var dto = (CarreraDto)dtoEntidad;

            var entidad = new Carrera
            {
                Descripcion = dto.Descripcion,
                CantidadCuotas = dto.CantidadCuotas,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.CarreraRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {

            var dto = (CarreraDto) dtoEntidad;

            var entidad = await _unidadDeTrabajo.CarreraRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró la carrera que quiere modificar");

            entidad.Descripcion = dto.Descripcion;
            entidad.CantidadCuotas = dto.CantidadCuotas;

            await _unidadDeTrabajo.CarreraRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(long id)
        {
            var entidad =  await _unidadDeTrabajo.CarreraRepositorio.Obtener(id);

            if (entidad == null) throw new Exception("No se encotró la carrera que esta buscando");

            return new CarreraDto
            {
                Id = entidad.Id,
                Descripcion = entidad.Descripcion,
                CantidadCuotas =  entidad.CantidadCuotas,
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos()
        {

            var entidad = await _unidadDeTrabajo.CarreraRepositorio.ObtenerTodos();

            return entidad.Select(x => new CarreraDto
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                CantidadCuotas = x.CantidadCuotas,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<Carrera, bool>> filtro = x => x.Descripcion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.CarreraRepositorio.Obtener(filtro);

            return entidad.Select(x => new CarreraDto
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                CantidadCuotas = x.CantidadCuotas,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }
    }
}
