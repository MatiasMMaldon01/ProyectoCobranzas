using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.PrecioCuota;
using IServicios.PrecioCuota.PrecioCuotaDTO;
using Servicios.Base;
using System.Linq.Expressions;

namespace Servicios.PrecioCuotaServicio
{
    public class PrecioCarreraServicio : IPrecioCarreraServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public PrecioCarreraServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
           _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.PrecioCarreraRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {

            var dto = (PrecioCarreraDTO)dtoEntidad;

            DateTime fecha = DateTime.Now;

            var entidad = new PrecioCarrera
            {
                Monto = dto.Monto,
                Matricula = dto.Matricula,
                Fecha = fecha,
                CarreraId = dto.CarreraId,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.PrecioCarreraRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {

            var dto = (PrecioCarreraDTO)dtoEntidad;

            var entidad = await _unidadDeTrabajo.PrecioCarreraRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró el precio que quiere modificar");

            entidad.Id = dto.Id;
            entidad.Matricula = dto.Matricula;
            entidad.Monto = dto.Monto;
            entidad.CarreraId = dto.CarreraId;
            

            await _unidadDeTrabajo.PrecioCarreraRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.PrecioCarreraRepositorio.Obtener(id, "Carrera");

            if (entidad == null) throw new Exception("No se encotró el precio que esta buscando");

            return new PrecioCarreraDTO
            {
                Id = entidad.Id,
                Monto = entidad.Monto,
                Matricula = entidad.Matricula,
                Fecha = entidad.Fecha,
                CarreraId = entidad.CarreraId,
                Carrera = entidad.Carrera.Descripcion,
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<PrecioCarrera, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var entidad = await _unidadDeTrabajo.PrecioCarreraRepositorio.ObtenerTodos(filtro, "Carrera");

            return entidad.Select(x => new PrecioCarreraDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                Fecha = x.Fecha,
                Matricula = x.Matricula,
                CarreraId = x.CarreraId,
                Carrera = x.Carrera.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Fecha)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<PrecioCarrera, bool>> filtro = x => x.Fecha.ToString().Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.PrecioCarreraRepositorio.Obtener(filtro, "Carrera");

            return entidad.Select(x => new PrecioCarreraDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                Matricula = x.Matricula,
                Fecha = x.Fecha,
                CarreraId = x.CarreraId,
                Carrera = x.Carrera.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Fecha)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> ObtenerPorCarreraId(int carreraId, bool mostrarTodos = false)
        {
            Expression<Func<PrecioCarrera, bool>> filtro = x => x.CarreraId == carreraId;

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.PrecioCarreraRepositorio.Obtener(filtro, "Carrera");

            return entidad.Select(x => new PrecioCarreraDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                Fecha = x.Fecha,
                Matricula = x.Matricula,
                CarreraId = x.CarreraId,
                Carrera = x.Carrera.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Fecha)
                .ToList();
        }
    }
}
