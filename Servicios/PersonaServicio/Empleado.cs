using Aplicacion.Constantes.Enums;
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
                Apynom = entidadNueva.Apynom,
                TipoDoc = entidadNueva.TipoDoc,
                NroDoc = entidadNueva.NroDoc,
                FechaNacimiento = entidadNueva.FechaNacimiento,
                Direccion = entidadNueva.Direccion,
                Mail = entidadNueva.Mail,
                Telefono = entidadNueva.Telefono,
                AreaTrabajo = entidadNueva.AreaTrabajo,
                CiudadId = entidadNueva.CiudadId,
                ExtensionId = entidadNueva.ExtensionId,
                EstaEliminado = false,
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
                Apynom = entidadModificar.Apynom,
                TipoDoc = entidadModificar.TipoDoc,
                NroDoc = entidadModificar.NroDoc,
                FechaNacimiento = entidadModificar.FechaNacimiento,
                Direccion = entidadModificar.Direccion,
                Mail = entidadModificar.Mail,
                Telefono = entidadModificar.Telefono,
                AreaTrabajo = entidadModificar.AreaTrabajo,
                CiudadId = entidadModificar.CiudadId,
                ExtensionId = entidadModificar.ExtensionId,
                EstaEliminado = false,
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
            Expression<Func<Dominio.Entidades.Empleado, bool>> filtro = empleado => (empleado.Apynom.Contains(cadenaBuscar)
                    || empleado.NroDoc == cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var respuesta = await _unidadDeTrabajo.EmpleadoRepositorio.Obtener(filtro, "Extension, Ciudad");

            return respuesta
                    .Select(x => new EmpleadoDTO
                    {
                        Id = x.Id,
                        Apynom = x.Apynom,
                        TipoDoc = x.TipoDoc,
                        NroDoc = x.NroDoc,
                        FechaNacimiento = x.FechaNacimiento,
                        Direccion = x.Direccion,
                        Mail = x.Mail,
                        Telefono = x.Telefono,
                        AreaTrabajo = x.AreaTrabajo,
                        CiudadId = x.CiudadId,
                        Ciudad = x.Ciudad.Descripcion,
                        ExtensionId = x.ExtensionId,
                        Extension =x.Extension.Descripcion,
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apynom).ThenBy(x => x.NroDoc)
                    .ToList();
        }

        public override async Task<PersonaDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.EmpleadoRepositorio.Obtener(id, "Extension, Ciudad");

            return new EmpleadoDTO
            {
                Id = entidad.Id,
                Apynom = entidad.Apynom,
                TipoDoc = entidad.TipoDoc,
                NroDoc = entidad.NroDoc,
                FechaNacimiento = entidad.FechaNacimiento,
                Direccion = entidad.Direccion,
                Mail = entidad.Mail,
                Telefono = entidad.Telefono,
                AreaTrabajo = entidad.AreaTrabajo,
                CiudadId = entidad.CiudadId,
                Ciudad = entidad.Ciudad.Descripcion,
                ExtensionId = entidad.ExtensionId,
                Extension = entidad.Extension.Descripcion,
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

            var respuesta = await _unidadDeTrabajo.EmpleadoRepositorio.ObtenerTodos(filtro, "Extension, Ciudad");

            return respuesta
                    .Select(x => new EmpleadoDTO
                    {
                        Id = x.Id,
                        Apynom = x.Apynom,
                        TipoDoc = x.TipoDoc,
                        NroDoc = x.NroDoc,
                        FechaNacimiento = x.FechaNacimiento,
                        Direccion = x.Direccion,
                        Mail = x.Mail,
                        Telefono = x.Telefono,
                        AreaTrabajo = x.AreaTrabajo,
                        CiudadId = x.CiudadId,
                        Ciudad = x.Ciudad.Descripcion,
                        ExtensionId = x.ExtensionId,
                        Extension = x.Extension.Descripcion,
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apynom).ThenBy(x => x.NroDoc)
                    .ToList();
        }
    }

}

