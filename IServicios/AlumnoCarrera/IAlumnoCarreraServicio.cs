using IServicios.Base.Base_DTO;

namespace IServicios.AlumnoCarrera
{
    public interface IAlumnoCarreraServicio
    {
        Task<long> Crear(BaseDTO dtoEntidad);
        Task Modificar(BaseDTO dtoEntidad);
        Task Eliminar(long id);
    }
}
