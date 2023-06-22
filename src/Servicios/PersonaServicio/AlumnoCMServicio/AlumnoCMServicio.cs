﻿using Aplicacion.Constantes.Enums;
using Dominio.Interfaces;
using IServicios.Contador;
using IServicios.Persona.CargasMasivas;
using SpreadsheetLight;
using System.Transactions;

namespace Servicios.PersonaServicio.AlumnoCMServicio
{
    public class AlumnoCMServicio : IAlumnoCargaMasiva
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IContadorServicio _contadorServicio;

        public AlumnoCMServicio(IUnidadDeTrabajo unidadDeTrabajo, IContadorServicio contadorServicio)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _contadorServicio = contadorServicio;
        }

        public async Task CargaMasivaAlumno()
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    List<Dominio.Entidades.Alumno> alumnos = new List<Dominio.Entidades.Alumno>();
                    List<Dominio.Entidades.Persona> personas = new List<Dominio.Entidades.Persona>();

                    string path = @"C:\Users\matia\OneDrive\Escritorio\Proyectos\CargaMasiva\CargaMasivaAlumnos.xlsx";
                    SLDocument document = new SLDocument(path);

                    int contador = await _contadorServicio.ObtenerSiguienteNumero(Entidad.Persona);
                    int fila = 2;

                    while (!string.IsNullOrEmpty(document.GetCellValueAsString(fila, 1)))
                    {

                        var persona = new Dominio.Entidades.Persona()
                        {
                            Id = contador,
                            Apynom = document.GetCellValueAsString(fila, 2),
                            TipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), document.GetCellValueAsString(fila, 3)),
                            NroDoc = document.GetCellValueAsString(fila, 4),
                            FechaNacimiento = document.GetCellValueAsDateTime(fila, 5),
                            Direccion = document.GetCellValueAsString(fila, 6),
                            CiudadId = document.GetCellValueAsInt32(fila, 7),
                            Telefono = document.GetCellValueAsString(fila, 10),
                            Mail = document.GetCellValueAsString(fila, 11),
                            ExtensionId = document.GetCellValueAsInt32(fila, 14),
                        };
                        var alumno = new Dominio.Entidades.Alumno()
                        {
                            Id = contador,
                            Legajo = document.GetCellValueAsString(fila, 1),
                            CarreraId = document.GetCellValueAsInt32(fila, 12),
                            FechaIngreso = document.GetCellValueAsDateTime(fila, 16)
                        };

                        alumnos.Add(alumno);
                        personas.Add(persona);
                        fila++;
                        contador++;
                    }

                    await _unidadDeTrabajo.CargaMasivaAlumnoRepositorio.CargaMasiva(alumnos);
                    await _unidadDeTrabajo.CargaMasivaPersonaRepositorio.CargaMasiva(personas);
                    await _contadorServicio.CargarNumero(Entidad.Persona, contador);

                    _unidadDeTrabajo.Commit();

                    tran.Complete();

                }
                catch
                {
                    tran.Dispose();
                    throw new Exception("Ocurrio un error grave al grabar los Alumnos");
                }
            }

                 
        }
    }
}