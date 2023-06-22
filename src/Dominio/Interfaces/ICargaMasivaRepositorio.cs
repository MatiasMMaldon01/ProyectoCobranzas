using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ICargaMasivaRepositorio<T> where T : EntidadBase
    {

        Task CargaMasiva(List<T> registros);
    }
}
