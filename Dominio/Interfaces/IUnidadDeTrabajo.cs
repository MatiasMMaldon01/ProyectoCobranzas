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
        IRepositorio<AlumnoCarrera> AlumnoCarreraRepositorio { get; }
        IEmpleadoRepositorio EmpleadoRepositorio { get; }
        IRepositorio<Pago> PagoRepositorio { get; }
        IRepositorio<Cuota> CuotaRepositorio { get; }
        IRepositorio<PrecioCuota> PrecioCuotaRepositorio { get; }
        IRepositorio<Usuario> UsuarioRepositorio { get; }


    }
}
