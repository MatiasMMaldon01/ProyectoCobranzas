using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Pago.PagoDTO;
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

        public override async Task<int> Crear(PersonaDTO entidad)
        {
            if (entidad == null)
                throw new Exception("Ocurrio un error al Insertar el Alumno");

            var entidadNueva = (AlumnoDTO)entidad;

            var alumno = new Dominio.Entidades.Alumno
            {
                Legajo = entidadNueva.Legajo,
                Apynom = entidadNueva.Apynom,
                TipoDoc = entidadNueva.TipoDoc,
                NroDoc = entidadNueva.NroDoc,
                FechaNacimiento = entidadNueva.FechaNacimiento,
                Direccion = entidadNueva.Direccion,
                Mail = entidadNueva.Mail,
                Telefono = entidadNueva.Telefono,
                PorcBeca = 0,
                FechaIngreso = entidadNueva.FechaIngreso,
                CiudadId = entidadNueva.CiudadId,
                ExtensionId = entidadNueva.ExtensionId,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.AlumnoRepositorio.Crear(alumno);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public override async Task Modificar(PersonaDTO entidad)
        {
            if (entidad == null)
                throw new Exception("Ocurrio un error al modificar el Alumno");

            var entidadModificar = (AlumnoDTO)entidad;

            await _unidadDeTrabajo.AlumnoRepositorio.Modificar(new Dominio.Entidades.Alumno
            {
                Id = entidadModificar.Id,
                Legajo = entidadModificar.Legajo,
                Apynom = entidadModificar.Apynom,
                TipoDoc = entidadModificar.TipoDoc,
                NroDoc = entidadModificar.NroDoc,
                FechaNacimiento = entidadModificar.FechaNacimiento,
                Direccion = entidadModificar.Direccion,
                Mail = entidadModificar.Mail,
                Telefono = entidadModificar.Telefono,
                PorcBeca = 0,
                FechaIngreso = entidadModificar.FechaIngreso,
                CiudadId = entidadModificar.CiudadId,
                ExtensionId = entidadModificar.ExtensionId,
                EstaEliminado = false,
            });
        }

        public override async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.AlumnoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public override async Task<IEnumerable<PersonaDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {

            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = alumno => alumno.Apynom.Contains(cadenaBuscar)
                    || alumno.NroDoc == cadenaBuscar
                    || alumno.Legajo == cadenaBuscar;

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var respuesta = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(filtro, "Pagos, Ciudad, Extension");

            return respuesta
                    .Select(x => new AlumnoDTO
                    {
                        Id = x.Id,
                        Legajo = x.Legajo,
                        Apynom = x.Apynom,
                        TipoDoc = x.TipoDoc,
                        NroDoc = x.NroDoc,
                        Direccion = x.Direccion,
                        FechaNacimiento = x.FechaNacimiento,
                        Mail = x.Mail,
                        Telefono = x.Telefono,
                        FechaIngreso = x.FechaIngreso,
                        ExtensionId = x.ExtensionId,
                        Extension = x.Extension.Descripcion,
                        CiudadId = x.CiudadId,
                        Ciudad = x.Ciudad.Descripcion,
                        Pagos = ManejoDePagos(x.Pagos.ToList()),
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Legajo).ThenBy(x => x.Apynom)
                    .ToList();
        }

        public override async Task<PersonaDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(id, "Pagos, Extension, Ciudad");

            return new AlumnoDTO
            {
                Id = entidad.Id,
                Legajo = entidad.Legajo,
                Apynom = entidad.Apynom,
                TipoDoc = entidad.TipoDoc,
                NroDoc = entidad.NroDoc,
                Direccion = entidad.Direccion,
                FechaNacimiento = entidad.FechaNacimiento,
                Mail = entidad.Mail,
                Telefono = entidad.Telefono,
                FechaIngreso = entidad.FechaIngreso,
                ExtensionId = entidad.ExtensionId,
                Extension = entidad.Extension.Descripcion,
                CiudadId = entidad.CiudadId,
                Ciudad = entidad.Ciudad.Descripcion,
                Pagos = ManejoDePagos(entidad.Pagos.ToList()),
                Eliminado = entidad.EstaEliminado,
            };
        }

        public override async Task<IEnumerable<PersonaDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = x => !x.EstaEliminado;

            var respuesta = await _unidadDeTrabajo.AlumnoRepositorio.ObtenerTodos(filtro, "Pagos, Extension, Ciudad");

            return respuesta
                    .Select(x => new AlumnoDTO
                    {
                        Id = x.Id,
                        Legajo = x.Legajo,
                        Apynom = x.Apynom,
                        TipoDoc = x.TipoDoc,
                        NroDoc = x.NroDoc,
                        Direccion = x.Direccion,
                        FechaNacimiento = x.FechaNacimiento,
                        Mail = x.Mail,
                        Telefono = x.Telefono,
                        FechaIngreso = x.FechaIngreso,
                        ExtensionId = x.ExtensionId,
                        Extension = x.Extension.Descripcion,
                        CiudadId = x.CiudadId,
                        Ciudad = x.Ciudad.Descripcion,
                        Pagos = ManejoDePagos(x.Pagos.ToList()),
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Legajo).ThenBy(x => x.Apynom)
                    .ToList();
        }

        // ==================================== METODOS PRIVADOS ==================================== //

        private List<PagoDTO> ManejoDePagos(List<Pago> pagos)
        {

            List<PagoDTO> pagosList = new List<PagoDTO>();

            foreach (var p in pagos)
            {
                if (!p.EstaEliminado)
                {
                    var pago = new PagoDTO
                    {
                        Id = p.Id,
                        Monto = p.Monto,
                        NroRecibo = p.NroRecibo,
                        FechaCarga = p.FechaCarga,
                        FechaRecibo = p.FechaRecibo,
                        CuotaId = p.CuotaId,
                        AlumnoId = p.AlumnoId,
                        Eliminado = p.EstaEliminado
                    };

                    pagosList.Add(pago);
                }
            }

            return pagosList;
        }

    }
}
