using IServicios.Base;
using IServicios.Base.Base_DTO;

namespace IServicios.Pago
{
    public interface IPagoServicio : IServicioBase
    {
        Task<IEnumerable<BaseDTO>> ObtenerPorAlumnoId(int alumnoId, bool mostrarTodos = true);
    }
}
