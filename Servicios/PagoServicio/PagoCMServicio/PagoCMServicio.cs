﻿using Aplicacion.Constantes.Enums;
using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Contador;
using IServicios.Pago.CargasMasivas;
using IServicios.Persona;
using SpreadsheetLight;
using System.Transactions;

namespace Servicios.PagoServicio.PagoCMServicio
{
    public class PagoCMServicio : IPagoCargaMasiva
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IContadorServicio _contadorServicio;
        private readonly IAlumnoServicio _alumnoServicio;

        public PagoCMServicio(IUnidadDeTrabajo unidadDeTrabajo, IContadorServicio contadorServicio, IAlumnoServicio alumnoServicio)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _contadorServicio = contadorServicio;
            _alumnoServicio = alumnoServicio;
        }

        public async Task CargaMasivaPago()
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    List<Pago> pagos = new List<Pago>();

                    string path = @"C:\Users\matia\OneDrive\Escritorio\Proyectos\CargaMasiva\CargaMasivaPago.xlsx";
                    SLDocument document = new SLDocument(path);

                    int contador = await _contadorServicio.ObtenerSiguienteNumero(Entidad.Pago);
                    int fila = 2;

                    while (!string.IsNullOrEmpty(document.GetCellValueAsString(fila, 1)))
                    {
                        var legajo = document.GetCellValueAsString(fila, 1);
                        var (cantidadPago, alumnoId) = await _alumnoServicio.ObtenerNroCuotasYId(legajo);
                        var cantidadDeCuotas = document.GetCellValueAsInt32(fila, 2);

                        for (int i = 0; i < cantidadDeCuotas; i++)
                        {
                            var pago = new Pago()
                            {
                                Id = contador,
                                Legajo = legajo,
                                NroCuota = cantidadPago + i,
                                FechaCarga = document.GetCellValueAsDateTime(fila, 3),
                                NroRecibo = document.GetCellValueAsInt32(fila, 4),
                                Monto = document.GetCellValueAsInt32(fila, 5) / cantidadDeCuotas,
                                FechaRecibo = document.GetCellValueAsDateTime(fila,6),
                                AlumnoId = alumnoId,
                            };

                            pagos.Add(pago);
                            contador++;
                        }
                        
                        fila++;

                    }

                    await _unidadDeTrabajo.CargaMasivaPagoRepositorio.CargaMasiva(pagos);
                    await _contadorServicio.CargarNumero(Entidad.Pago, contador);

                    _unidadDeTrabajo.Commit();

                    tran.Complete();

                }
                catch
                {
                    tran.Dispose();
                    throw new Exception("Ocurrio un error grave al grabar los Pagos");
                }
            }
        }
    }
}
