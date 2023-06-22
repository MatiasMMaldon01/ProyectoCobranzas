using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IUnidadDeTrabajo
    {
        void Commit();

        void Disposed();


        // Declaraciones de los Repositorios

        IRepositorio<Carrera> CarreraRepositorio { get; }
        IAlumnoRepositorio AlumnoRepositorio { get; }
        IEmpleadoRepositorio EmpleadoRepositorio { get; }
        IRepositorio<Pago> PagoRepositorio { get; }
        IRepositorio<PrecioCarrera> PrecioCarreraRepositorio { get; }
        IRepositorio<Usuario> UsuarioRepositorio { get; }
        IRepositorio<Ciudad> CiudadRepositorio { get; }
        IRepositorio<Extension> ExtensionRepositorio { get; }
        IRepositorio<Contador> ContadorRepositorio { get; }

        // Repositorios de Carga Masiva
        ICargaMasivaRepositorio<Alumno> CargaMasivaAlumnoRepositorio { get; }
        ICargaMasivaRepositorio<Persona> CargaMasivaPersonaRepositorio { get; }
        ICargaMasivaRepositorio<Pago> CargaMasivaPagoRepositorio { get; }



    }
}
