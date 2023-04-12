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
                CantidadCuotas = dto.cantCuotas,
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
            entidad.CantidadCuotas = dto.cantCuotas;

            await _unidadDeTrabajo.CarreraRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(long id)
        {
            var entidad =  await _unidadDeTrabajo.CarreraRepositorio.Obtener(id, "PrecioCuota");

            if (entidad == null) throw new Exception("No se encotró la carrera que esta buscando");

            return new CarreraDto
            {
                Id = entidad.Id,
                Descripcion = entidad.Descripcion,
                cantCuotas =  entidad.CantidadCuotas,
               // Fecha = entidad.Fecha,
                precioCuo = entidad.PrecioCuota.Monto,
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

            var entidad = await _unidadDeTrabajo.CarreraRepositorio.ObtenerTodos(filtro, "PrecioCuota");

            return entidad.Select(x => new CarreraDto
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                cantCuotas = x.CantidadCuotas,
               // Fecha = x.Fecha,
                precioCuo = x.PrecioCuota != null ? x.PrecioCuota.Monto : 0 ,
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

            var entidad = await _unidadDeTrabajo.CarreraRepositorio.Obtener(filtro, "PrecioCuota");

            return entidad.Select(x => new CarreraDto
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                cantCuotas = x.CantidadCuotas,
                Fecha = x.Fecha,
                precioCuo = x.PrecioCuota.Monto,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }
    }
}
