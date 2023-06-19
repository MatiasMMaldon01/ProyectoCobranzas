using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Cuota;
using IServicios.Cuota.CuotaDTO;
using IServicios.Pago.PagoDTO;
using IServicios.PrecioCuota.PrecioCuotaDTO;
using Servicios.Base;
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

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.CuotaRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {
            var dto = (CuotaDTO)dtoEntidad;

            DateTime fecha = DateTime.Now;
            int contador = 0;

            Cuota entidad = new Cuota();

            var precioCarr = await _unidadDeTrabajo.PrecioCarreraRepositorio.Obtener(dto.PrecioCarreraId, "Carrera");

            for(int i=0; i <= precioCarr.Carrera.CantidadCuotas; i++)
            {
                if(contador == 0)
                {
                    entidad = new Cuota
                    {
                        Numero = contador++,
                        MontoCuota = precioCarr.Matricula,
                        Fecha = fecha,
                        PrecioCarreraId = precioCarr.Id,
                    };
                }
                else
                {
                    entidad = new Cuota
                    {
                        Numero = contador++,
                        MontoCuota = precioCarr.Monto,
                        Fecha = fecha,
                        PrecioCarreraId = precioCarr.Id,
                    };
                }

                await _unidadDeTrabajo.CuotaRepositorio.Crear(entidad);

            }

            _unidadDeTrabajo.Commit();

            return 1;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {
            var dto = (CuotaDTO)dtoEntidad;

            var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró la cuota que quiere modificar");

            entidad.Id = dto.Id;
            entidad.MontoCuota = dto.MontoCuota;
            entidad.Numero = dto.Numero;
            entidad.PrecioCarreraId = dto.PrecioCarreraId;

            await _unidadDeTrabajo.CuotaRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(id, "PrecioCarrera.Carrera, Pagos");

            if (entidad == null) throw new Exception("No se encotró la cuota que esta buscando");

            return new CuotaDTO
            {
                Id = entidad.Id,
                Numero = entidad.Numero,
                MontoCuota = entidad.MontoCuota,
                Fecha = entidad.Fecha,
                PrecioCarreraId = entidad.PrecioCarreraId,
                PrecioCarrera = new PrecioCarreraDTO {
                    Id = entidad.PrecioCarrera.Id,
                    Monto = entidad.PrecioCarrera.Monto,
                    Fecha = entidad.PrecioCarrera.Fecha,
                    CarreraId = entidad.PrecioCarrera.CarreraId,
                    Carrera = entidad.PrecioCarrera.Carrera.Descripcion,
                },

                NumeroDePagos = entidad.Pagos.Count(x => !x.EstaEliminado),
                Pagos = ManejoDePagos(entidad.Pagos.ToList()),
                Eliminado = entidad.EstaEliminado,
            };
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<Cuota, bool>> filtro = x => x.Numero.ToString() == cadenaBuscar || x.PrecioCarrera.Carrera.Descripcion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(filtro, "PrecioCarrera.Carrera, Pagos");

            return entidad.Select(x => new CuotaDTO
            {
                Id = x.Id,
                MontoCuota = x.MontoCuota,
                Numero = x.Numero,
                Fecha = x.Fecha,
                PrecioCarreraId = x.PrecioCarreraId,
                PrecioCarrera = new PrecioCarreraDTO
                {
                    Id = x.PrecioCarrera.Id,
                    Monto = x.PrecioCarrera.Monto,
                    Fecha = x.PrecioCarrera.Fecha,
                    CarreraId = x.PrecioCarrera.CarreraId,
                    Carrera = x.PrecioCarrera.Carrera.Descripcion,
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

            var entidad = await _unidadDeTrabajo.CuotaRepositorio.ObtenerTodos(filtro, "PrecioCarrera.Carrera, Pagos");

            return entidad.Select(x => new CuotaDTO
            {
                Id = x.Id,
                MontoCuota = x.MontoCuota,
                Numero = x.Numero,
                Fecha = x.Fecha,
                PrecioCarreraId = x.PrecioCarreraId,
                PrecioCarrera = new PrecioCarreraDTO
                {
                    Id = x.PrecioCarrera.Id,
                    Monto = x.PrecioCarrera.Monto,
                    Fecha = x.PrecioCarrera.Fecha,
                    CarreraId = x.PrecioCarrera.CarreraId,
                    Carrera = x.PrecioCarrera.Carrera.Descripcion,
                },
                NumeroDePagos = x.Pagos.Count(x => !x.EstaEliminado),
                Pagos = ManejoDePagos(x.Pagos.ToList()),
                Eliminado = x.EstaEliminado,
            })
                .OrderBy(x => x.Numero)
                .ToList();
        }

        //public async Task<BaseDTO> UltimaCuotaAlumno(int alumnoId, int precioCuotaId, bool mostrarTodos = false)
        //{
        //    Expression<Func<Cuota, bool>> filtro = x => x.AlumnoId == alumnoId & x.PrecioCuotaId == precioCuotaId;

        //    if (!mostrarTodos)
        //    {
        //        filtro = filtro.And(x => !x.EstaEliminado);
        //    }

        //    var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(filtro, "PrecioCuota.Carrera, Alumno, Pagos");

        //    if (entidad.Count() == 0)
        //    {
        //        return null;
        //    }

        //    Cuota ultimaCuota = entidad.OrderBy(x => x.Fecha).LastOrDefault();

        //    return new CuotaDTO
        //    {
        //        Id = ultimaCuota.Id,
        //        Numero = ultimaCuota.Numero,
        //        PorcAbonado = ultimaCuota.PorcAbonado,
        //        Fecha = ultimaCuota.Fecha,
        //        EstadoCuo = ultimaCuota.EstadoCuota,
        //        PrecioCuotaId = ultimaCuota.PrecioCuotaId,
        //        PrecioCuota = new PrecioCarreraDTO
        //        {
        //            Id = ultimaCuota.PrecioCuota.Id,
        //            Monto = ultimaCuota.PrecioCuota.Monto,
        //            Fecha = ultimaCuota.PrecioCuota.Fecha,
        //            CarreraId = ultimaCuota.PrecioCuota.CarreraId,
        //            Carrera = ultimaCuota.PrecioCuota.Carrera.Descripcion,
        //        },
        //        AlumnoId = ultimaCuota.AlumnoId,
        //        Alumno = new AlumnoDTO
        //        {
        //            Id = ultimaCuota.Alumno.Id,
        //            Legajo = ultimaCuota.Alumno.Legajo,
        //            Nombre = ultimaCuota.Alumno.Nombre,
        //            Apellido = ultimaCuota.Alumno.Apellido,
        //            Dni = ultimaCuota.Alumno.Dni,
        //            Mail = ultimaCuota.Alumno.Mail,
        //            FechaIngreso = ultimaCuota.Alumno.FechaIngreso,

        //        },
        //        NumeroDePagos = ultimaCuota.Pagos.Count(x => !x.EstaEliminado),
        //        Pagos = ManejoDePagos(ultimaCuota.Pagos.ToList()),
        //        Eliminado = ultimaCuota.EstaEliminado,
        //    };

        //}

        //public async Task<IEnumerable<BaseDTO>> ObtenerPorCarreraIdAlumnoId(int alumnoId, int carreraId, bool mostrarTodos = false)
        //{
        //    Expression<Func<Cuota, bool>> filtro = x => x.AlumnoId == alumnoId & x.PrecioCuota.CarreraId == carreraId;

        //    if (!mostrarTodos)
        //    {
        //        filtro = filtro.And(x => !x.EstaEliminado);
        //    }

        //    var entidad = await _unidadDeTrabajo.CuotaRepositorio.Obtener(filtro, "PrecioCuota.Carrera, Alumno, Pagos");

        //    return entidad.Select(x => new CuotaDTO
        //    {
        //        Id = x.Id,
        //        Numero = x.Numero,
        //        PorcAbonado = x.PorcAbonado,
        //        Fecha = x.Fecha,
        //        EstadoCuo = x.EstadoCuota,
        //        PrecioCuotaId = x.PrecioCuotaId,
        //        PrecioCuota = new PrecioCarreraDTO
        //        {
        //            Id = x.PrecioCuota.Id,
        //            Monto = x.PrecioCuota.Monto,
        //            Fecha = x.PrecioCuota.Fecha,
        //            CarreraId = x.PrecioCuota.CarreraId,
        //            Carrera = x.PrecioCuota.Carrera.Descripcion,
        //        },
        //        AlumnoId = x.AlumnoId,
        //        Alumno = new AlumnoDTO
        //        {
        //            Id = x.Alumno.Id,
        //            Legajo = x.Alumno.Legajo,
        //            Nombre = x.Alumno.Nombre,
        //            Apellido = x.Alumno.Apellido,
        //            Dni = x.Alumno.Dni,
        //            Mail = x.Alumno.Mail,
        //            FechaIngreso = x.Alumno.FechaIngreso,

        //        },
        //        NumeroDePagos = x.Pagos.Count(x => !x.EstaEliminado),
        //        Pagos = ManejoDePagos(x.Pagos.ToList()),
        //        Eliminado = x.EstaEliminado,
        //    })
        //       .OrderBy(x => x.Numero)
        //       .ToList();
        //}

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
