using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Pago;
using IServicios.Pago.PagoDTO;
using IServicios.Persona;
using Servicios.Base;
using System.Linq.Expressions;
using System.Transactions;

namespace Servicios.PagoServicio
{
    public class PagoServicio : IPagoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IAlumnoServicio _alumnoServicio;

        public PagoServicio(IUnidadDeTrabajo unidadDeTrabajo, IAlumnoServicio alumnoServicio)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _alumnoServicio = alumnoServicio;
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Pago pago = new Pago();
                    var dto = (PagoDTO)dtoEntidad;

                    var (cantidadPagadas, alumnoId) = await _alumnoServicio.ObtenerNroCuotasYId(dto.Legajo);

                    for (int i = 0; i < dto.CantCuota; i++)
                    {
                        pago = new Pago
                        {
                            Legajo = dto.Legajo,
                            Monto = dto.Monto / dto.CantCuota,
                            NroCuota = cantidadPagadas + i,
                            FechaCarga = dto.FechaCarga,
                            FechaRecibo = dto.FechaRecibo,
                            NroRecibo = dto.NroRecibo,
                            AlumnoId = alumnoId,
                        };

                        await _unidadDeTrabajo.PagoRepositorio.Crear(pago);
                    }

                    _unidadDeTrabajo.Commit();

                    tran.Complete();
                    return pago.Id;
                }
                catch
                {
                    tran.Dispose();
                    throw new Exception("Ocurrio un error grave al grabar el Pago");
                }
            }

        }

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.PagoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public Task Modificar(BaseDTO dtoEntidad)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseDTO> Obtener(int id)
        {
            var pago = await _unidadDeTrabajo.PagoRepositorio.Obtener(id);

            if (pago == null) throw new Exception("No se encotró el pago que esta buscando");

            return new PagoDTO
            {
                Id = pago.Id,
                Legajo = pago.Legajo,
                NroCuota = pago.NroCuota,
                NroRecibo = pago.NroRecibo,
                Monto = pago.Monto,
                FechaCarga = pago.FechaCarga,
                FechaRecibo = pago.FechaRecibo,
                AlumnoId = pago.AlumnoId,
                Eliminado = pago.EstaEliminado,

            };
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<Pago, bool>> filtro = x => x.NroRecibo.ToString() == cadenaBuscar;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var pagos = await _unidadDeTrabajo.PagoRepositorio.Obtener(filtro);

            return pagos.Select(x => new PagoDTO
            {
                Id = x.Id,
                NroCuota = x.NroCuota,
                Legajo = x.Legajo,
                Monto = x.Monto,
                NroRecibo = x.NroRecibo,
                FechaRecibo = x.FechaRecibo,
                FechaCarga = x.FechaCarga,
                AlumnoId = x.AlumnoId,
                Eliminado = x.EstaEliminado,
            }).OrderBy(x => x.Legajo)
            .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> ObtenerPorAlumnoId(int alumnoId, bool mostrarTodos = false)
        {
            Expression<Func<Pago, bool>> filtro = x => x.AlumnoId == alumnoId;

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.PagoRepositorio.Obtener(filtro);

            if (!entidad.Any()) throw new Exception("No se encotro ningún pago del Alumno");

            return entidad.Select(x => new PagoDTO
            {
                Id = x.Id,
                NroCuota = x.NroCuota,
                Legajo = x.Legajo,
                Monto = x.Monto,
                NroRecibo = x.NroRecibo,
                FechaRecibo = x.FechaRecibo,
                FechaCarga = x.FechaCarga,
                AlumnoId = x.AlumnoId,
                Eliminado = x.EstaEliminado,
            })
               .OrderBy(x => x.NroCuota)
               .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            Expression<Func<Pago, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var pagos = await _unidadDeTrabajo.PagoRepositorio.ObtenerTodos(filtro);

            return pagos.Select(x => new PagoDTO
            {
                Id = x.Id,
                NroCuota = x.NroCuota,
                Legajo = x.Legajo,
                Monto = x.Monto,
                NroRecibo = x.NroRecibo,
                FechaRecibo = x.FechaRecibo,
                FechaCarga = x.FechaCarga,
                AlumnoId = x.AlumnoId,
                Eliminado = x.EstaEliminado,
            })
                .OrderBy(x => x.FechaRecibo)
                .ToList();
        }
    }
    
}
