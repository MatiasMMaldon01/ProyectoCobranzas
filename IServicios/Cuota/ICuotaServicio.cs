using IServicios.Base;
using IServicios.Base.Base_DTO;

namespace IServicios.Cuota
{
    public interface ICuotaServicio : IServicioBase
    {
        Task<BaseDTO> UltimaCuotaAlumno(int alumnoId, int precioCuotaId, bool mostrarTodos = false);

        Task<IEnumerable<BaseDTO>> ObtenerPorCarreraIdAlumnoId(int alumnoId, int carreraId, bool mostrarTodos = false);
    }
}
