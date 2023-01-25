using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.PrecioCuota;
using IServicios.PrecioCuota.PrecioCuotaDTO;
using Servicios.Base;
using System.Linq.Expressions;

namespace Servicios.PrecioCuotaServicio
{
    public class PrecioCuotaServicio : IPrecioCuotaServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public PrecioCuotaServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
           _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Eliminar(long id)
        {
            await _unidadDeTrabajo.PrecioCuotaRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<long> Crear(BaseDTO dtoEntidad)
        {

            var dto = (PrecioCuotaDTO)dtoEntidad;

            DateTime fecha = DateTime.Now;

            var entidad = new PrecioCuota
            {
                Monto = dto.Monto,
                Fecha = fecha,
                CarreraId = dto.CarreraId,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.PrecioCuotaRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {

            var dto = (PrecioCuotaDTO)dtoEntidad;

            var entidad = await _unidadDeTrabajo.PrecioCuotaRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró el precio que quiere modificar");

            entidad.Id = dto.Id;
            entidad.Monto = dto.Monto;
            entidad.CarreraId = dto.CarreraId;
            

            await _unidadDeTrabajo.PrecioCuotaRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(long id)
        {
            var entidad = await _unidadDeTrabajo.PrecioCuotaRepositorio.Obtener(id, "Carrera");

            if (entidad == null) throw new Exception("No se encotró el precio que esta buscando");

            return new PrecioCuotaDTO
            {
                Id = entidad.Id,
                Monto = entidad.Monto,
                Fecha = entidad.Fecha,
                CarreraId = entidad.CarreraId,
                Carrera = entidad.Carrera.Descripcion,
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos()
        {

            var entidad = await _unidadDeTrabajo.PrecioCuotaRepositorio.ObtenerTodos("Carrera");

            return entidad.Select(x => new PrecioCuotaDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                Fecha = x.Fecha,
                CarreraId = x.CarreraId,
                Carrera = x.Carrera.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Fecha)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<PrecioCuota, bool>> filtro = x => x.Fecha.ToString().Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.PrecioCuotaRepositorio.Obtener(filtro, "Carrera");

            return entidad.Select(x => new PrecioCuotaDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                Fecha = x.Fecha,
                CarreraId = x.CarreraId,
                Carrera = x.Carrera.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Fecha)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> ObtenerPorCarreraId(long carreraId, bool mostrarTodos = true)
        {
            Expression<Func<PrecioCuota, bool>> filtro = x => x.CarreraId == carreraId;

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.PrecioCuotaRepositorio.Obtener(filtro, "Carrera");

            return entidad.Select(x => new PrecioCuotaDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                Fecha = x.Fecha,
                CarreraId = x.CarreraId,
                Carrera = x.Carrera.Descripcion,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Fecha)
                .ToList();
        }
    }
}
