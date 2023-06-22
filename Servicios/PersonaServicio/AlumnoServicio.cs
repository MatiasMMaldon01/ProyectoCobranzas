using Dominio.Interfaces;
using IServicios.Persona;
using System.Linq.Expressions;

namespace Servicios.PersonaServicio
{
    public class AlumnoServicio : PersonaServicio, IAlumnoServicio
    {
        public AlumnoServicio(IUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo)
        {
            
        }

        public async Task<(int, int)> ObtenerNroCuotasYId(string legajo)
        {
            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = alumno => alumno.Legajo == legajo;

            var alumnos = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(filtro, "Pagos");
            var alumno = alumnos.FirstOrDefault();

            if (alumno == null || alumno.EstaEliminado)
            {
                throw new Exception("No se encontró el alumno");
            }

            var cantidadDeCuotas = alumno.Pagos == null? 0 : alumno.Pagos.Where(x => !x.EstaEliminado).ToList().Count();
            var alumnoId = alumno.Id;

            return (cantidadDeCuotas, alumnoId);
        }
    }
}
