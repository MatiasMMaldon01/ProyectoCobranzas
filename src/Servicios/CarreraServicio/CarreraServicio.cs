using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Carrera;
using IServicios.Carrera.Carrera_DTO;
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

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.CarreraRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {

            var dto = (CarreraDto)dtoEntidad;

            var entidad = new Carrera
            {
                Descripcion = dto.Descripcion,
                CantidadCuotas = dto.CantCuotas,
                Fecha = DateTime.Now,
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
            entidad.CantidadCuotas = dto.CantCuotas;

            await _unidadDeTrabajo.CarreraRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(int id)
        {
            var entidad =  await _unidadDeTrabajo.CarreraRepositorio.Obtener(id, "PrecioCarreras");

            if (entidad == null) throw new Exception("No se encotró la carrera que esta buscando");

            return new CarreraDto
            {
                Id = entidad.Id,
                Descripcion = entidad.Descripcion,
                CantCuotas =  entidad.CantidadCuotas,
                Fecha = entidad.Fecha,
                PrecioCarrera = entidad.PrecioCarreras != null?entidad.PrecioCarreras.LastOrDefault().Monto : 0,
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<Carrera, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var entidad = await _unidadDeTrabajo.CarreraRepositorio.ObtenerTodos(filtro, "PrecioCarreras");

            return entidad.Select(x => new CarreraDto
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                CantCuotas = x.CantidadCuotas,
                Fecha = x.Fecha,
                PrecioCarrera = x.PrecioCarreras != null ? x.PrecioCarreras.LastOrDefault().Monto : 0,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<Carrera, bool>> filtro = x => x.Descripcion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.CarreraRepositorio.Obtener(filtro, "PrecioCarreras");

            return entidad.Select(x => new CarreraDto
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                CantCuotas = x.CantidadCuotas,
                Fecha = x.Fecha,
                PrecioCarrera = x.PrecioCarreras != null ? x.PrecioCarreras.LastOrDefault().Monto : 0,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }
    }
}
