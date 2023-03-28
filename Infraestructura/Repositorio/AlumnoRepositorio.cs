using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructura.Repositorio
{
    public class AlumnoRepositorio : Repositorio<Alumno>, IAlumnoRepositorio
    {
        public AlumnoRepositorio(DataContext context) : base(context)
        {
        }

        public override async Task<Alumno> Obtener(long id, string propiedadesNavegacion = "")
        {
            var query = propiedadesNavegacion.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                 .Aggregate<string, IQueryable<Alumno>>(_context.Set<Persona>().OfType<Alumno>(), (current, include) => current.Include(include.Trim()));

            var resultado = await query.FirstOrDefaultAsync(x => x.Id == id);

            if (resultado == null)
                throw new Exception("El objeto que intenta buscar no existe");

            return resultado;
        }

        public override async Task<IEnumerable<Alumno>> Obtener(Expression<Func<Alumno, bool>> filtro = null, string propiedadesNavegacion = "")
        {
            var resultado = propiedadesNavegacion.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate<string, IQueryable<Alumno>>(_context.Set<Persona>().OfType<Alumno>(), (current, include) => current.Include(include.Trim()));

            if (filtro != null) resultado = resultado.Where(filtro);

            return resultado.ToList();
        }

        public override async Task<IEnumerable<Alumno>> ObtenerTodos(Expression<Func<Alumno, bool>> filtro = null, string propiedadesNavegacion = "")
        {
            var query = propiedadesNavegacion.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate<string, IQueryable<Alumno>>(_context.Set<Persona>().OfType<Alumno>(), (current, include) => current.Include(include.Trim()));

            return await query.ToListAsync();
        }
    }
}
