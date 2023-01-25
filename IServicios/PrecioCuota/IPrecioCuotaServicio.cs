using IServicios.Base;
using IServicios.Base.Base_DTO;

namespace IServicios.PrecioCuota
{
    public interface IPrecioCuotaServicio : IServicioBase
    {
        public Task<IEnumerable<BaseDTO>> ObtenerPorCarreraId(long carreraId, bool mostrarTodos = true);
    }
}
