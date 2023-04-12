using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Carrera.Carrera_DTO;
using IServicios.Persona.DTO_s;
using Servicios.Base;
using System.Linq.Expressions;
using System.Transactions;

namespace Servicios.PersonaServicio
{
    internal class Alumno : Persona
    {
        public Alumno(IUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo)
        {
        }

        public override async Task<long> Crear(PersonaDTO entidad)
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (entidad == null)
                        throw new Exception("Ocurrio un error al Insertar el Alumno");

                    var entidadNueva = (AlumnoDTO)entidad;

                    var alumno = new Dominio.Entidades.Alumno
                    {
                        EstaEliminado = false,
                        Legajo = entidadNueva.Legajo,
                        Apellido = entidadNueva.Apellido,
                        Nombre = entidadNueva.Nombre,
                        Dni = entidadNueva.Dni,
                        Direccion = entidadNueva.Direccion,
                        Mail = entidadNueva.Mail,
                        PorcBeca = entidadNueva.PorcBeca,
                        Telefono = entidadNueva.Telefono,
                        FechaIngreso = entidadNueva.FechaIngreso,
                    };

                    await _unidadDeTrabajo.AlumnoRepositorio.Crear(alumno);

                    _unidadDeTrabajo.Commit();


                    foreach(var c in entidadNueva.Carreras)
                    {
                        var alumnoCarrera = new AlumnoCarrera
                        {
                            AlumnoId = alumno.Id,
                            CarreraId = c.Id,
                        };

                       await _unidadDeTrabajo.AlumnoCarreraRepositorio.Crear(alumnoCarrera);
                    }

                    _unidadDeTrabajo.Commit();
                    
                    tran.Complete();

                    return alumno.Id;
                }
                catch
                {
                    tran.Dispose();
                    throw new Exception("Ocurrió un error grave al grabar el Alumno");
                }
            }               
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
                PorcBeca = entidadModificar.PorcBeca,
                Nombre = entidadModificar.Nombre,
                Telefono = entidadModificar.Telefono,
                FechaIngreso = entidadModificar.FechaIngreso,
            });
        }

        public override async Task Eliminar(long id)
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var entidadEliminar = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(id, "AlumnoCarreras");

                    if (entidadEliminar == null) throw new Exception("No se encontro el Alumno a eliminar");

                    await _unidadDeTrabajo.AlumnoRepositorio.Eliminar(id);

                    foreach(var alumnoCarrera in entidadEliminar.AlumnoCarreras)
                    {
                        await _unidadDeTrabajo.AlumnoCarreraRepositorio.Eliminar(alumnoCarrera.Id);
                    }

                    _unidadDeTrabajo.Commit();
                    tran.Complete();
                }
                catch
                {
                    tran.Dispose();
                    throw new Exception("Ocurrió un error grave al eliminar el Alumno");
                }
            }
        }

        public override async Task<IEnumerable<PersonaDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = alumno => (alumno.Apellido.Contains(cadenaBuscar)
                    || alumno.Nombre.Contains(cadenaBuscar)
                    || alumno.Dni == cadenaBuscar
                    || alumno.Legajo == int.Parse(cadenaBuscar));

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var respuesta = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(filtro, "AlumnoCarreras.Carrera, AlumnoCarreras.Carrera.PrecioCuota");

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
                        PorcBeca = x.PorcBeca,
                        Carreras = ManejarCarreras(x.AlumnoCarreras.ToList()),
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                    .ToList();
        }

        public override async Task<PersonaDTO> Obtener(long id)
        {
            var entidad = await _unidadDeTrabajo.AlumnoRepositorio.Obtener(id, "AlumnoCarreras.Carrera, AlumnoCarreras.Carrera.PrecioCuota");

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
                PorcBeca = entidad.PorcBeca,
                FechaIngreso = entidad.FechaIngreso,
                Carreras = ManejarCarreras(entidad.AlumnoCarreras.ToList()),
                Eliminado = entidad.EstaEliminado,
            };
        }

        public override async Task<IEnumerable<PersonaDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<Dominio.Entidades.Alumno, bool>> filtro = x => x.EstaEliminado || !x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var respuesta = await _unidadDeTrabajo.AlumnoRepositorio.ObtenerTodos(filtro, "AlumnoCarreras.Carrera, AlumnoCarreras.Carrera.PrecioCuota");

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
                        PorcBeca = x.PorcBeca,
                        FechaIngreso = x.FechaIngreso,
                        Carreras = ManejarCarreras(x.AlumnoCarreras.ToList()),
                        Eliminado = x.EstaEliminado,
                    }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                    .ToList();
        }

        // ==================================== METODOS PRIVADOS ==================================== //

        private List<CarreraDto> ManejarCarreras(List<AlumnoCarrera> alumnoCarreras)
        {

            List<CarreraDto> carrerasList = new List<CarreraDto>();
            foreach(var carrera in alumnoCarreras)
            {
                var carreraAlumno = new CarreraDto
                {
                    Id = carrera.Carrera.Id,
                    Descripcion = carrera.Carrera.Descripcion,
                    PrecioCuo = carrera.Carrera.PrecioCuota.Monto,
                    CantCuotas = carrera.Carrera.CantidadCuotas,
                };

                carrerasList.Add(carreraAlumno);
            }

            return carrerasList;
        }
    }
}
