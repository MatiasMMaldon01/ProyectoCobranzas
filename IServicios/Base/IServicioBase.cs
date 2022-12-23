using IServicios.Base.Base_DTO;

namespace IServicios.Base
{
    public interface IServicioBase
    {
        Task<long> Crear(BaseDTO dtoEntidad);

        Task Modificar(BaseDTO dtoEntidad);

        Task Eliminar(long id);

        Task<BaseDTO> Obtener(long id);

        Task<IEnumerable<BaseDTO>> ObtenerTodos();

        Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = true);
    }
}
