using IServicios.Base.Base_DTO;

namespace IServicios.AlumnoCarrera
{
    public interface IAlumnoCarreraServicio
    {
        Task<int> Crear(BaseDTO dtoEntidad);
        Task Modificar(BaseDTO dtoEntidad);
        Task Eliminar(int id);
    }
}
