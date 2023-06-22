using Dominio.Entidades;
using System.Linq.Expressions;

namespace Dominio.Interfaces
{
    public interface IRepositorio<T> where T : EntidadBase
    {

        // Persistencia de Datos

        Task<int> Crear(T entidad);
        Task Modificar(T entidad);
        Task Eliminar(int id);

        // Metodos de Lectura

        Task<T> Obtener(int id, string propiedadesNavegacion = "");

        Task<IEnumerable<T>> Obtener(Expression<Func<T, bool>> filtro = null, string propiedadesNavegacion = "");

        Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, string propiedadesNavegacion = "");


    }
}
