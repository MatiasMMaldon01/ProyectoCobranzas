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
        IRepositorio<Cuota> CuotaRepositorio { get; }
        IRepositorio<PrecioCarrera> PrecioCarreraRepositorio { get; }
        IRepositorio<Usuario> UsuarioRepositorio { get; }
        IRepositorio<Ciudad> CiudadRepositorio { get; }
        IRepositorio<Extension> ExtensionRepositorio { get; }

    }
}
