using IServicios.Base;
using IServicios.Base.Base_DTO;

namespace IServicios.Cuota
{
    public interface ICuotaServicio : IServicioBase
    {
        Task<BaseDTO> UltumaCuotaAlumno(long alumnoId, bool mostrarTodos = false);
    }
}
