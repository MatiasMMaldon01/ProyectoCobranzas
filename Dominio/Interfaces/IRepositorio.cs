using Dominio.Entidades;
using System.Linq.Expressions;

namespace Dominio.Interfaces
{
    public interface IRepositorio<T> where T : EntidadBase
    {

        // Persistencia de Datos

        Task<long> Crear(T entidad);
        Task Modificar(T entidad);
        Task Eliminar(long id);

        // Metodos de Lectura

        Task<T> Obtener(long id, string propiedadesNavegacion = "");

        Task<IEnumerable<T>> ObtenerTodos(string propiedadesNavegacion = "");

        Task<IEnumerable<T>> Obtener(Expression<Func<T, bool>> filtro = null, string propiedadesNavegacion = "");


    }
}
