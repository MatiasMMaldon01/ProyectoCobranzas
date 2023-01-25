using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructura.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : EntidadBase
    {
        protected readonly DataContext _context;
        public Repositorio(DataContext context)
        {   
            _context = context;
        }
        #region Metodos Persistencia
        public async Task<long> Crear(T entidad)
        {

            if(entidad == null) throw new Exception("La entidad que quiere crear no tiene valores");

            await _context.Set<T>().AddAsync(entidad);

             return entidad.Id;
        }

        public async Task Eliminar(long id)
        {

            var entidadEliminar = _context.Set<T>().FirstOrDefault(x => x.Id == id);

            if (entidadEliminar == null) throw new Exception("La entidad a eliminar no existe");

            entidadEliminar.EstaEliminado = !entidadEliminar.EstaEliminado;

        }

        public async Task Modificar(T entidad)
        {
            if (entidad == null) throw new Exception("La entidad que quiere modificar no tiene valores");

            _context.Set<T>().Attach(entidad);
            _context.Entry(entidad).State = EntityState.Modified;
        }

        #endregion

        #region Metodos Obtener
        public virtual async Task<T> Obtener(long id, string propiedadesNavegacion = "")
        {
            var query = propiedadesNavegacion.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate<string, IQueryable<T>>(_context.Set<T>(), (current, include) => current.Include(include.Trim()));

            var resultado = await query.FirstOrDefaultAsync(x => x.Id == id);

            if (resultado == null)
                throw new Exception("El objeto que intenta buscar no existe");

            return resultado;
        }
        
        public virtual async Task<IEnumerable<T>> Obtener(Expression<Func<T, bool>> filtro = null, string propiedadesNavegacion = "")
        {

            var query = propiedadesNavegacion.Split(new[] { ',' },StringSplitOptions.RemoveEmptyEntries).
                Aggregate<string,IQueryable<T>>(_context.Set<T>(), (current, include) => current.Include(include.Trim()));

            if (filtro != null) query = query.Where(filtro);

            var resultado = await query.ToListAsync();

            return resultado;
        }

        public virtual async Task<IEnumerable<T>> ObtenerTodos(string propiedadesNavegacion = "")
        {
            var query = propiedadesNavegacion.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).
               Aggregate<string, IQueryable<T>>(_context.Set<T>(), (current, include) => current.Include(include.Trim()));

            return await query.ToListAsync();
        }

        #endregion
    }
}
