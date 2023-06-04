using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Persona.DTO_s;
using Servicios.Base;
using System.Linq.Expressions;

namespace Servicios.PersonaServicio
{
    public class Empleado : Persona
    {
        public Empleado(IUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo)
        {
        }

        public override async Task<int> Crear(PersonaDTO entidad)
        {
            if (entidad == null)
                throw new Exception("Ocurrio un error al Insertar el Empleado");

            var entidadNueva = (EmpleadoDTO)entidad;

            var entidadId = await _unidadDeTrabajo.EmpleadoRepositorio.Crear(new Dominio.Entidades.Empleado
            {
                EstaEliminado = false,
                Apellido = entidadNueva.Apellido,
                Nombre = entidadNueva.Nombre,
                Dni = entidadNueva.Dni,
                Direccion = entidadNueva.Direccion,
                Mail = entidadNueva.Mail,
                Telefono = entidadNueva.Telefono,
                AreaTrabajo = entidadNueva.AreaTrabajo
            });

            _unidadDeTrabajo.Commit();

            return entidadId;
        }

        public override async Task Modificar(PersonaDTO entidad)
        {
            if (entidad == null)
                throw new Exception("Ocurrio un error al modificar el Empleado");

            var entidadModificar = (EmpleadoDTO)entidad;

            await _unidadDeTrabajo.EmpleadoRepositorio.Modificar(new Dominio.Entidades.Empleado
            {
                Id = entidadModificar.Id,
                EstaEliminado = false,
                Apellido = entidadModificar.Apellido,
                Direccion = entidadModificar.Direccion,
                Dni = entidadModificar.Dni,
                Mail = entidadModificar.Mail,
                Nombre = entidadModificar.Nombre,
                Telefono = entidadModificar.Telefono,
                AreaTrabajo = entidadModificar.AreaTrabajo
            });

            _unidadDeTrabajo.Commit();
        }

        public override async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.EmpleadoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public override async Task<IEnumerable<PersonaDTO>> Obtener(string cadenaBuscar, bool mostrarTodos)
        {
            Expression<Func<Dominio.Entidades.Empleado, bool>> filtro = empleado => (empleado.Apellido.Contains(cadenaBuscar)
                    || empleado.Nombre.Contains(cadenaBuscar)
                    || empleado.Dni == cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var respuesta = await _unidadDeTrabajo.EmpleadoRepositorio.Obtener(filtro);

            return respuesta
                    .Select(x => new EmpleadoDTO
                    {
                        Id = x.Id,
                        Apellido = x.Apellido,
                        Direccion = x.Direccion,
                        Dni = x.Dni,
                        Mail = x.Mail,
                        Nombre = x.Nombre,
                        Telefono = x.Telefono,
                        AreaTrabajo = x.AreaTrabajo,
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                    .ToList();
        }

        public override async Task<PersonaDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.EmpleadoRepositorio.Obtener(id);

            return new EmpleadoDTO
            {
                Id = entidad.Id,
                Apellido = entidad.Apellido,
                Direccion = entidad.Direccion,
                Dni = entidad.Dni,
                Mail = entidad.Mail,
                Nombre = entidad.Nombre,
                Telefono = entidad.Telefono,
                AreaTrabajo = entidad.AreaTrabajo,
                Eliminado = entidad.EstaEliminado,
            };
        }

        public override async Task<IEnumerable<PersonaDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<Dominio.Entidades.Empleado, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var respuesta = await _unidadDeTrabajo.EmpleadoRepositorio.ObtenerTodos(filtro);

            return respuesta
                    .Select(x => new EmpleadoDTO
                    {
                        Id = x.Id,
                        Apellido = x.Apellido,
                        Direccion = x.Direccion,
                        Dni = x.Dni,
                        Mail = x.Mail,
                        Nombre = x.Nombre,
                        Telefono = x.Telefono,
                        AreaTrabajo = x.AreaTrabajo,
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                    .ToList();
        }
    }

}

