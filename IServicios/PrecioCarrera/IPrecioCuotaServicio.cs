using IServicios.Base;
using IServicios.Base.Base_DTO;

namespace IServicios.PrecioCuota
{
    public interface IPrecioCarreraServicio : IServicioBase
    {
        public Task<IEnumerable<BaseDTO>> ObtenerPorCarreraId(int carreraId, bool mostrarTodos = true);
    }
}
