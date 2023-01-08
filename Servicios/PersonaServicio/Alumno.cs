using Dominio.Interfaces;
using IServicios.Persona.DTO_s;
using Servicios.Base;
using System.Linq.Expressions;

namespace Servicios.PersonaServicio
{
    internal class Alumno : Persona
    {
        public Alumno(IUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo)
        {
        }

        public override async Task<long> Crear(PersonaDTO entidad)
        {
            if (entidad == null)
                throw new Exception("Ocurrio un error al Insertar el Alumno");

            var entidadNueva = (AlumnoDTO)entidad;

            var entidadId = await _unidadDeTrabajo.AlumnoRepositorio.Crear(new Dominio.Entidades.Alumno
            {
                EstaEliminado = false,
                Legajo = entidadNueva.Legajo,
                Apellido = entidadNueva.Apellido,
                Nombre = entidadNueva.Nombre,
                Dni = entidadNueva.Dni,
                Direccion = entidadNueva.Direccion,
                Mail = entidadNueva.Mail,
                Telefono = entidadNueva.Telefono,
                FechaIngreso = entidadNueva.FechaIngreso,
                CarreraId = entidadNueva.CarreraId,
            });

            _unidadDeTrabajo.Commit();

            return entidadId;
        }

        public override async Task Modificar(PersonaDTO entidad)
        {
            if (entidad == null)
                throw new Exception("Ocurrio un error al modificar el Alumno");

            var entidadModificar = (AlumnoDTO)entidad;

            await _unidadDeTrabajo.AlumnoRepositorio.Modificar(new Dominio.Entidades.Alumno
            {
                Id = entidadModificar.Id,
                EstaEliminado = false,
                Legajo = entidadModificar.Legajo,
                Apellido = entidadModificar.Apellido,
                Direccion = entidadModificar.Direccion,
                Dni = entidadModificar.Dni,
                Mail = entidadModificar.Mail,
                Nombre = entidadModificar.Nombre,
                Telefono = entidadModificar.Telefono,
                FechaIngreso = entidadModificar.FechaIngreso,
                CarreraId = entidadModificar.CarreraId,
            });

            _unidadDeTrabajo.Commit();
        }

        public override async Task Eliminar(long id)
        {
            await _unidadDeTrabajo.AlumnoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public override async Task<IEnumerable<PersonaDTO>> Obtener(string cadenaBuscar, bool mostrarTodos)
        {
            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = alumno => (alumno.Apellido.Contains(cadenaBuscar)
                    || alumno.Nombre.Contains(cadenaBuscar)
                    || alumno.Dni == cadenaBuscar
                    || alumno.Legajo == int.Parse(cadenaBuscar));

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var respuesta = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(filtro, "Carrera");

            return respuesta
                    .Select(x => new AlumnoDTO
                    {
                        Id = x.Id,
                        Legajo = x.Legajo,
                        Apellido = x.Apellido,
                        Direccion = x.Direccion,
                        Dni = x.Dni,
                        Mail = x.Mail,
                        Nombre = x.Nombre,
                        Telefono = x.Telefono,
                        FechaIngreso = x.FechaIngreso,
                        CarreraId = x.CarreraId,
                        Carrera = x.Carrera.Descripcion,
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                    .ToList();
        }

        public override async Task<PersonaDTO> Obtener(long id)
        {
            var entidad = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(id, "Carrera");

            return new AlumnoDTO
            {
                Id = entidad.Id,
                Legajo = entidad.Legajo,
                Apellido = entidad.Apellido,
                Direccion = entidad.Direccion,
                Dni = entidad.Dni,
                Mail = entidad.Mail,
                Nombre = entidad.Nombre,
                Telefono = entidad.Telefono,
                FechaIngreso = entidad.FechaIngreso,
                CarreraId = entidad.CarreraId,
                Carrera = entidad.Carrera.Descripcion,
                Eliminado = entidad.EstaEliminado,
            };
        }

        public override async Task<IEnumerable<PersonaDTO>> ObtenerTodos()
        {

            var respuesta = await _unidadDeTrabajo.AlumnoRepositorio.ObtenerTodos("Carrera");

            return respuesta
                    .Select(x => new AlumnoDTO
                    {
                        Id = x.Id,
                        Legajo = x.Legajo,
                        Apellido = x.Apellido,
                        Direccion = x.Direccion,
                        Dni = x.Dni,
                        Mail = x.Mail,
                        Nombre = x.Nombre,
                        Telefono = x.Telefono,
                        FechaIngreso = x.FechaIngreso,
                        CarreraId = x.CarreraId,
                        Carrera = x.Carrera.Descripcion,
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                    .ToList();
        }
    }
}
