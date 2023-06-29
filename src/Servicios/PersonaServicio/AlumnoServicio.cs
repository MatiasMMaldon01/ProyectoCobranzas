using Dominio.Interfaces;
using IServicios.Persona;
using IServicios.Persona.DTO_s;
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

        // ==================================== FILTRO DE ALUMNOS ==================================== //

        public async Task<IEnumerable<FiltroAlumnosDTO>> FiltrarAlumnos(int corte)
        {
            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = alumno => !alumno.EstaEliminado &&
                              ((alumno.Pagos.Count <= 10 && alumno.Pagos.Any(c => c.NroCuota == corte))
                              || (alumno.Pagos.Count > 10 && alumno.Pagos.Count <= 20 && alumno.Pagos.Any(c => c.NroCuota == 10 + corte))
                              || (alumno.Pagos.Count > 20 && alumno.Pagos.Count <= 30 && alumno.Pagos.Any(c => c.NroCuota == 20 + corte))
                              || (alumno.Pagos.Count > 30 && alumno.Pagos.Count <= 40 && alumno.Pagos.Any(c => c.NroCuota == 30 + corte)));

            var alumnos = await _unidadDeTrabajo.AlumnoRepositorio.ObtenerTodos(filtro, "Pagos, Extension, Ciudad, Carrera");

            //var alumnosActivos = from alumno in alumnos
            //                     where (alumno.Pagos.Count < 10 && alumno.Pagos.Any(c => c.NroCuota == corte))
            //                         || (alumno.Pagos.Count > 10 && alumno.Pagos.Count <= 20 && alumno.Pagos.Any(c => c.NroCuota == 10 + corte))
            //                         || (alumno.Pagos.Count > 20 && alumno.Pagos.Count <= 30 && alumno.Pagos.Any(c => c.NroCuota == 20 + corte))
            //                         || (alumno.Pagos.Count > 30 && alumno.Pagos.Count <= 40 && alumno.Pagos.Any(c => c.NroCuota == 30 + corte))
            //                     select alumno;

            return alumnos.Select(x => new FiltroAlumnosDTO()
            {
                Apynom = x.Apynom,
                Legajo = x.Legajo,
                Mail = x.Mail,
                NroDoc = x.NroDoc,
                Carrera = x.Carrera.Descripcion,
                Extension = x.Extension.Descripcion,
                Activo = true,
            }).ToList();
        }
    }
}
