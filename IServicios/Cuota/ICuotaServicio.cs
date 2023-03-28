using IServicios.Base;
using IServicios.Base.Base_DTO;

namespace IServicios.Cuota
{
    public interface ICuotaServicio : IServicioBase
    {
        Task<BaseDTO> UltimaCuotaAlumno(long alumnoId, long precioCuotaId, bool mostrarTodos = false);

        Task<IEnumerable<BaseDTO>> ObtenerPorCarreraIdAlumnoId(long alumnoId, long carreraId, bool mostrarTodos = false);
    }
}
