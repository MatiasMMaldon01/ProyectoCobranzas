using Aplicacion.Constantes.Enums;
using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Cuota;
using IServicios.Cuota.CuotaDTO;
using IServicios.Pago.PagoDTO;
using IServicios.Persona.DTO_s;
using IServicios.PrecioCuota.PrecioCuotaDTO;
using Servicios.Base;
using System.Linq;
using System.Linq.Expressions;

namespace Servicios.CuotaServicio
{
    public class CuotaServicio : ICuotaServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public CuotaServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Eliminar(long id)
        {
            await _unidadDeTrabajo.CuotaRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<long> Crear(BaseDTO dtoEntidad)
        {
            var dto = (CuotaDTO)dtoEntidad;

            DateTime fecha = DateTime.Now;

            var entidad = new Cuota
            {
                Numero = dto.Numero,
                MontoAbonado = 0,
                Fecha = fecha,
                EstadoCuota = EstadoCuota.Pendiente,
                PrecioCuotaId = dto.PrecioCuotaId,
                AlumnoId = dto.AlumnoId,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.CuotaRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {
            var dto = (CuotaDTO)dtoEntidad;

            var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró la cuota que quiere modificar");

            entidad.Id = dto.Id;
            entidad.Numero = dto.Numero;
            entidad.EstadoCuota = dto.EstadoCuota;
            entidad.PrecioCuotaId = dto.PrecioCuotaId;
            entidad.AlumnoId = dto.AlumnoId;


            await _unidadDeTrabajo.CuotaRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(long id)
        {
            var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(id, "PrecioCuota.Carrera, Alumno.Carrera, Pagos");

            if (entidad == null) throw new Exception("No se encotró la cuota que esta buscando");

            return new CuotaDTO
            {
                Id = entidad.Id,
                Numero = entidad.Numero,
                MontoAbonado = entidad.MontoAbonado,
                Fecha = entidad.Fecha,
                EstadoCuota = entidad.EstadoCuota,
                PrecioCuotaId = entidad.PrecioCuotaId,
                PrecioCuota = new PrecioCuotaDTO {
                    Id = entidad.PrecioCuota.Id,
                    Monto = entidad.PrecioCuota.Monto,
                    Fecha = entidad.PrecioCuota.Fecha,
                    CarreraId = entidad.PrecioCuota.CarreraId,
                    Carrera = entidad.PrecioCuota.Carrera.Descripcion,
                },
                AlumnoId = entidad.AlumnoId,
                Alumno = new AlumnoDTO
                {
                    Id = entidad.Alumno.Id,
                    Legajo = entidad.Alumno.Legajo,
                    Nombre = entidad.Alumno.Nombre,
                    Apellido = entidad.Alumno.Apellido,
                    Dni = entidad.Alumno.Dni,
                    Mail = entidad.Alumno.Mail,
                    FechaIngreso = entidad.Alumno.FechaIngreso,
                    Carrera = entidad.Alumno.Carrera.Descripcion

                },
                NumeroDePagos = entidad.Pagos.Count(x => !x.EstaEliminado),
                Pagos = ManejoDePagos(entidad.Pagos.ToList()),
                Eliminado = entidad.EstaEliminado,
            };
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<Cuota, bool>> filtro = x => x.Numero.ToString() == cadenaBuscar;

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(filtro, "PrecioCuota.Carrera, Alumno.Carrera, Pagos");

            return entidad.Select(x => new CuotaDTO
            {
                Id = x.Id,
                Numero = x.Numero,
                MontoAbonado = x.MontoAbonado,
                Fecha = x.Fecha,
                EstadoCuota = x.EstadoCuota,
                PrecioCuotaId = x.PrecioCuotaId,
                PrecioCuota = new PrecioCuotaDTO
                {
                    Id = x.PrecioCuota.Id,
                    Monto = x.PrecioCuota.Monto,
                    Fecha = x.PrecioCuota.Fecha,
                    CarreraId = x.PrecioCuota.CarreraId,
                    Carrera = x.PrecioCuota.Carrera.Descripcion,
                },
                AlumnoId = x.AlumnoId,
                Alumno = new AlumnoDTO
                {
                    Id = x.Alumno.Id,
                    Legajo = x.Alumno.Legajo,
                    Nombre = x.Alumno.Nombre,
                    Apellido = x.Alumno.Apellido,
                    Dni = x.Alumno.Dni,
                    Mail = x.Alumno.Mail,
                    FechaIngreso = x.Alumno.FechaIngreso,
                    Carrera = x.Alumno.Carrera.Descripcion

                },
                NumeroDePagos = x.Pagos.Count(x => !x.EstaEliminado),
                Pagos = ManejoDePagos(x.Pagos.ToList()),
                Eliminado = x.EstaEliminado,
            })
                .OrderBy(x => x.Numero)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {

            Expression<Func<Cuota, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var entidad = await _unidadDeTrabajo.CuotaRepositorio.ObtenerTodos(filtro, "PrecioCuota.Carrera, Alumno.Carrera, Pagos");

            return entidad.Select(x => new CuotaDTO
            {
                Id = x.Id,
                Numero = x.Numero,
                MontoAbonado = x.MontoAbonado,
                Fecha = x.Fecha,
                EstadoCuota = x.EstadoCuota,
                PrecioCuotaId = x.PrecioCuotaId,
                PrecioCuota = new PrecioCuotaDTO
                {
                    Id = x.PrecioCuota.Id,
                    Monto = x.PrecioCuota.Monto,
                    Fecha = x.PrecioCuota.Fecha,
                    CarreraId = x.PrecioCuota.CarreraId,
                    Carrera = x.PrecioCuota.Carrera.Descripcion,
                },
                AlumnoId = x.AlumnoId,
                Alumno = new AlumnoDTO
                {
                    Id = x.Alumno.Id,
                    Legajo = x.Alumno.Legajo,
                    Nombre = x.Alumno.Nombre,
                    Apellido = x.Alumno.Apellido,
                    Dni = x.Alumno.Dni,
                    Mail = x.Alumno.Mail,
                    FechaIngreso = x.Alumno.FechaIngreso,
                    Carrera = x.Alumno.Carrera.Descripcion

                },
                NumeroDePagos = x.Pagos.Count(x => !x.EstaEliminado),
                Pagos = ManejoDePagos(x.Pagos.ToList()),
                Eliminado = x.EstaEliminado,
            })
                .OrderBy(x => x.Numero)
                .ToList();
        }

        public async Task<BaseDTO> UltumaCuotaAlumno(long alumnoId, bool mostrarTodos = false)
        {
            Expression<Func<Cuota, bool>> filtro = x => x.AlumnoId == alumnoId & x.EstadoCuota == EstadoCuota.Pendiente;

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(filtro, "PrecioCuota.Carrera, Alumno.Carrera, Pagos");

            Cuota ultimaCuota = entidad.OrderBy(x => x.Fecha).LastOrDefault();

            return new CuotaDTO
            {
                Id = ultimaCuota.Id,
                Numero = ultimaCuota.Numero,
                MontoAbonado = ultimaCuota.MontoAbonado,
                Fecha = ultimaCuota.Fecha,
                EstadoCuota = ultimaCuota.EstadoCuota,
                PrecioCuotaId = ultimaCuota.PrecioCuotaId,
                PrecioCuota = new PrecioCuotaDTO
                {
                    Id = ultimaCuota.PrecioCuota.Id,
                    Monto = ultimaCuota.PrecioCuota.Monto,
                    Fecha = ultimaCuota.PrecioCuota.Fecha,
                    CarreraId = ultimaCuota.PrecioCuota.CarreraId,
                    Carrera = ultimaCuota.PrecioCuota.Carrera.Descripcion,
                },
                AlumnoId = ultimaCuota.AlumnoId,
                Alumno = new AlumnoDTO
                {
                    Id = ultimaCuota.Alumno.Id,
                    Legajo = ultimaCuota.Alumno.Legajo,
                    Nombre = ultimaCuota.Alumno.Nombre,
                    Apellido = ultimaCuota.Alumno.Apellido,
                    Dni = ultimaCuota.Alumno.Dni,
                    Mail = ultimaCuota.Alumno.Mail,
                    FechaIngreso = ultimaCuota.Alumno.FechaIngreso,
                    Carrera = ultimaCuota.Alumno.Carrera.Descripcion

                },
                NumeroDePagos = ultimaCuota.Pagos.Count(x => !x.EstaEliminado),
                Pagos = ManejoDePagos(ultimaCuota.Pagos.ToList()),
                Eliminado = ultimaCuota.EstaEliminado,
            };



        }

        // =========================================== Metodos Privados =========================================== //
        private List<PagoDTO> ManejoDePagos(List<Pago> pagos)
        {

            List<PagoDTO> pagosList = new List<PagoDTO>();

            foreach(var p in pagos)
            {
                if (!p.EstaEliminado)
                {
                    var pago =  new PagoDTO
                    {
                        Id = p.Id,
                        Monto = p.Monto,
                        FechaPago = p.FechaPago,
                        CuotaId = p.CuotaId,
                        Eliminado = p.EstaEliminado
                    };

                    pagosList.Add(pago);
                }
            }

            return pagosList;
        }

        //private decimal CalcularMontoAbonado(Cuota entidad)
        //{
        //    decimal montoAbonado = 0;

        //    foreach(var pago in entidad.Pagos)
        //    {
        //        montoAbonado += pago.Monto;
        //    }

        //    return montoAbonado;
        //}

    }
}
