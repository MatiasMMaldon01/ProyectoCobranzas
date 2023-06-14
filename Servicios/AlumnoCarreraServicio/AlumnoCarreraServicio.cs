using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.AlumnoCarrera;
using IServicios.AlumnoCarrera.AlumnoCarreraDTO;
using IServicios.Base.Base_DTO;

namespace Servicios.AlumnoCarreraServicio
{
    public class AlumnoCarreraServicio : IAlumnoCarreraServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public AlumnoCarreraServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {
            var dto = (AlumnoCarreraDTO)dtoEntidad;

            var entidad = new AlumnoCarrera
            {
                AlumnoId = dto.AlumnoId,
                CarreraId = dto.CarreraId,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.AlumnoCarreraRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {
            var dto = (AlumnoCarreraDTO) dtoEntidad;

            var entidad = await _unidadDeTrabajo.AlumnoCarreraRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró la carrera que quiere modificar");

            entidad.CarreraId = dto.CarreraId;
            entidad.AlumnoId = dto.AlumnoId;

            await _unidadDeTrabajo.AlumnoCarreraRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.AlumnoCarreraRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }
    }
}
