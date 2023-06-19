using IServicios.Base.Base_DTO;

namespace IServicios.Base
{
    public interface IServicioBase
    {
        Task<int> Crear(BaseDTO dtoEntidad);

        Task Modificar(BaseDTO dtoEntidad);

        Task Eliminar(int id);

        Task<BaseDTO> Obtener(int id);

        Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false);

        Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false);
    }
}
