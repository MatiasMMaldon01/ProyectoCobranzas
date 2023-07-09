using Dominio.Interfaces;
using IServicios.Persona;
using IServicios.Persona.DTO_s;
using SpreadsheetLight;
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
            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = alumno => !alumno.EstaEliminado;

            var alumnos = await _unidadDeTrabajo.AlumnoRepositorio.ObtenerTodos(filtro, "Pagos, Extension, Ciudad, Carrera");

            // Alumnos que pueden rendir
            var alumnosActivos = (from alumno in alumnos
                                 where (alumno.Pagos.Where(x => !x.EstaEliminado).Count() <= 10 && alumno.Pagos.Any(c => c.NroCuota == corte))
                                     || (alumno.Pagos.Where(x => !x.EstaEliminado).Count() > 10 && alumno.Pagos.Where(x => !x.EstaEliminado).Count() <= 20 && alumno.Pagos.Any(c => c.NroCuota == 10 + corte))
                                     || (alumno.Pagos.Where(x => !x.EstaEliminado).Count() > 20 && alumno.Pagos.Where(x => !x.EstaEliminado).Count() <= 30 && alumno.Pagos.Any(c => c.NroCuota == 20 + corte))
                                     || (alumno.Pagos.Where(x => !x.EstaEliminado).Count() > 30 && alumno.Pagos.Where(x => !x.EstaEliminado).Count() <= 40 && alumno.Pagos.Any(c => c.NroCuota == 30 + corte))
                                 select alumno).ToList();

            string path = @$"C:\Reportes\Cohorte-Fecha{(corte + 2).ToString("00")}-{DateTime.Now.Year}.xlsx";

            SLDocument oSLDocument = new SLDocument();

            System.Data.DataTable dt = new System.Data.DataTable();

            // Columnas
            dt.Columns.Add("Apellido y Nombre", typeof(string));
            dt.Columns.Add("Legajo", typeof(string));
            dt.Columns.Add("Mail", typeof(string));
            dt.Columns.Add("Nro Documento", typeof(string));
            dt.Columns.Add("Carrera", typeof(string));
            dt.Columns.Add("Extension", typeof(string));
            dt.Columns.Add("Activo", typeof(string));

            // Filas
            foreach(var alumno in alumnosActivos)
            {
                dt.Rows.Add(alumno.Apynom, alumno.Legajo, alumno.Mail, alumno.NroDoc,
                    alumno.Carrera.Descripcion, alumno.Extension.Descripcion, "PUEDE RENDIR");
            }

            decimal morosidad = 1 - ((decimal)alumnosActivos.Count() / (decimal)alumnos.Count());


            dt.Rows.Add();
            dt.Rows.Add($"Los alumnos que estan con las cuotas al día son: {alumnosActivos.Count()}");
            dt.Rows.Add($"Los alumnos que no pueden rendir son: {alumnos.Count() - alumnosActivos.Count()}");
            dt.Rows.Add($"El porcentaje de morosidad es del {morosidad * 100}%");

            oSLDocument.ImportDataTable(1,1,dt,true);

            oSLDocument.SaveAs(path);


            return alumnosActivos.Select(x => new FiltroAlumnosDTO()
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
