using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infraestructura.Repositorio
{
    public class EmpleadoRepositorio : Repositorio<Empleado>, IEmpleadoRepositorio
    {
        public EmpleadoRepositorio(DataContext dataContext)
            : base(dataContext)
        {

        }

        public override async Task<Empleado> Obtener(int id, string propiedadesNavegacion = "")
        {
            var query = propiedadesNavegacion.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                 .Aggregate<string, IQueryable<Empleado>>(_context.Set<Persona>().OfType<Empleado>(), (current, include) => current.Include(include.Trim()));

            var resultado = await query.FirstOrDefaultAsync(x => x.Id == id);

            if (resultado == null)
                throw new Exception("El objeto que intenta buscar no existe");

            return resultado;
        }

        public override async Task<IEnumerable<Empleado>> Obtener(Expression<Func<Empleado, bool>> filtro = null, string propiedadesNavegacion = "")
        {
            var resultado = propiedadesNavegacion.Split(new[] { "," },StringSplitOptions.RemoveEmptyEntries)
                .Aggregate<string,IQueryable<Empleado>>(_context.Set<Persona>().OfType<Empleado>(), (current, include) => current.Include(include.Trim()));

            if (filtro != null) resultado = resultado.Where(filtro);

            return resultado.ToList();
        }

        public override async Task<IEnumerable<Empleado>> ObtenerTodos(Expression<Func<Empleado, bool>> filtro = null, string propiedadesNavegacion = "")
        {
            var resultado = propiedadesNavegacion.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate<string, IQueryable<Empleado>>(_context.Set<Persona>().OfType<Empleado>(), (current, include) => current.Include(include.Trim()));

            if (filtro != null) resultado = resultado.Where(filtro);

            return await resultado.ToListAsync();
        }
    }
}
