using Aplicacion.Constantes.Enums;
using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Pago;
using IServicios.Pago.PagoDTO;
using Servicios.Base;
using System.Linq.Expressions;
using System.Transactions;

namespace Servicios.PagoServicio
{
    public class PagoServicio : IPagoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public PagoServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<long> Crear(BaseDTO dtoEntidad)
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var dto = (PagoDTO) dtoEntidad;

                    if (dto.Monto == 0) throw new Exception("El valor del Pago no puede ser CERO");

                    var cuota = await _unidadDeTrabajo.CuotaRepositorio.Obtener(dto.CuotaId, "PrecioCuota.Carrera, Alumno.Carrera, Pagos");

                    if (cuota.EstadoCuota == EstadoCuota.Pagada)
                    {
                        throw new Exception("Esta cuota esta abonada de forma integra");
                    }
                    else if ((cuota.MontoAbonado + dto.Monto) > cuota.PrecioCuota.Monto)
                    {
                        throw new Exception("Esta pagando de mas");
                    }

                    DateTime fecha = DateTime.Now;

                    var entidad = new Pago
                    {
                        Monto = dto.Monto,
                        FechaPago = fecha,
                        CuotaId = dto.CuotaId,
                        EstaEliminado = false
                    };

                    await _unidadDeTrabajo.PagoRepositorio.Crear(entidad);

                    Cuota cuotaPagada;

                    if ((cuota.MontoAbonado + entidad.Monto) == cuota.PrecioCuota.Monto)
                    {
                        cuotaPagada = new Cuota
                        {
                            Id = cuota.Id,
                            Numero = cuota.Numero,
                            EstadoCuota = EstadoCuota.Pagada,
                            PrecioCuotaId = cuota.PrecioCuotaId,
                            AlumnoId = cuota.AlumnoId,
                            MontoAbonado = cuota.MontoAbonado + entidad.Monto,
                        };
                    }
                    else
                    {
                        cuotaPagada = new Cuota
                        {
                            Id = cuota.Id,
                            Numero = cuota.Numero,
                            EstadoCuota = EstadoCuota.Pendiente,
                            PrecioCuotaId = cuota.PrecioCuotaId,
                            AlumnoId = cuota.AlumnoId,
                            MontoAbonado = cuota.MontoAbonado + entidad.Monto,
                        };

                    }

                    await _unidadDeTrabajo.CuotaRepositorio.Modificar(cuotaPagada);

                    _unidadDeTrabajo.Commit();

                    tran.Complete();

                    return entidad.Id;
                    
                }
                catch 
                {
                    tran.Dispose();
                    throw new Exception("Ocurrio un error grave al grabar el Pago");
                }               
            }         

        }

        public async Task Eliminar(long id)
        {
            await _unidadDeTrabajo.PagoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {

            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var dto = (PagoDTO)dtoEntidad;

                    if (dto.Monto == 0) throw new Exception("El valor del Pago no puede ser CERO");

                    var pago =  await _unidadDeTrabajo.PagoRepositorio.Obtener(dto.Id);

                    if (pago == null) throw new Exception("No se encotró el pago que quiere modificar");

                    var cuota = await _unidadDeTrabajo.CuotaRepositorio.Obtener(dto.CuotaId, "PrecioCuota.Carrera, Alumno.Carrera, Pagos");

                    if (cuota == null) throw new Exception("No se encontro la cuota de referencia");

                    var montoActualAbonado = (cuota.MontoAbonado - pago.Monto) + dto.Monto;

                    if (montoActualAbonado > cuota.PrecioCuota.Monto)
                    {
                        throw new Exception("Esta pagando de mas");
                    }


                    pago.Monto = dto.Monto;

                    await _unidadDeTrabajo.PagoRepositorio.Modificar(pago);

                    Cuota cuotaPagada;


                    if ((montoActualAbonado) == cuota.PrecioCuota.Monto)
                    {
                        cuotaPagada = new Cuota
                        {
                            Id = cuota.Id,
                            Numero = cuota.Numero,
                            EstadoCuota = EstadoCuota.Pagada,
                            PrecioCuotaId = cuota.PrecioCuotaId,
                            AlumnoId = cuota.AlumnoId,
                            MontoAbonado = montoActualAbonado,
                        };
                    }
                    else
                    {
                        cuotaPagada = new Cuota
                        {
                            Id = cuota.Id,
                            Numero = cuota.Numero,
                            EstadoCuota = EstadoCuota.Pendiente,
                            PrecioCuotaId = cuota.PrecioCuotaId,
                            AlumnoId = cuota.AlumnoId,
                            MontoAbonado = montoActualAbonado,
                        };

                    }

                    await _unidadDeTrabajo.CuotaRepositorio.Modificar(cuotaPagada);

                    _unidadDeTrabajo.Commit();

                    tran.Complete();
                }
                catch
                {
                    tran.Dispose();
                    throw new Exception("Ocurrio un error grave al modificar el Pago");
                }

            }
        }

        public async Task<BaseDTO> Obtener(long id)
        {
            var entidad = await _unidadDeTrabajo.PagoRepositorio.Obtener(id, "Cuota.Alumno, Cuota.PrecioCuota");

            if (entidad == null) throw new Exception("No se encotró el pago que esta buscando");

            return new PagoDTO
            {
                Id = entidad.Id,
                Monto = entidad.Monto,
                CuotaId = entidad.CuotaId,
                FechaPago = entidad.FechaPago,
                
                Eliminado = entidad.EstaEliminado,
            };
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos()
        {
            var entidad = await _unidadDeTrabajo.PagoRepositorio.ObtenerTodos("Cuota.Alumno, Cuota.PrecioCuota");

            return entidad.Select(x => new PagoDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                CuotaId = x.CuotaId,
                FechaPago = x.FechaPago,
                
                Eliminado = x.EstaEliminado,
            })
                .OrderBy(x => x.FechaPago)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> ObtenerPorAlumnoId(long alumnoId, bool mostrarTodos)
        {
            Expression<Func<Pago, bool>> filtro = x => x.Cuota.AlumnoId == alumnoId;

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }
            var entidad = await _unidadDeTrabajo.PagoRepositorio.Obtener();

            if (entidad == null) throw new Exception("No se encotro ningún pago del Alumno");

            return entidad.Select(x => new PagoDTO
            {
                Id = x.Id,
                Monto = x.Monto,
                CuotaId = x.CuotaId,
                FechaPago = x.FechaPago,
                Eliminado = x.EstaEliminado,
            })
               .OrderBy(x => x.CuotaId)
               .ToList();
        }
    }
    
}
