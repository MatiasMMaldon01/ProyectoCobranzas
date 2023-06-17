using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Data;
using Infraestructura.Repositorio;
//using System.Data.Entity.Validation;

namespace Infraestructura.UnidadDeTrabajo
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly DataContext _context;

        public UnidadDeTrabajo(DataContext context)
        {
            _context = context;
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Disposed()
        {
            _context.Dispose();
        }


        // ====================================================================================================================================== //
        public IRepositorio<Carrera> _carreraRepositorio;

        public IRepositorio<Carrera> CarreraRepositorio => _carreraRepositorio ?? (_carreraRepositorio = new Repositorio<Carrera>(_context));

        // ====================================================================================================================================== //

        public IAlumnoRepositorio _alumnoRepositorio;

        public IAlumnoRepositorio AlumnoRepositorio => _alumnoRepositorio ?? (_alumnoRepositorio = new AlumnoRepositorio(_context));

        // ====================================================================================================================================== //

        public IEmpleadoRepositorio _empleadoRepositorio;

        public IEmpleadoRepositorio EmpleadoRepositorio => _empleadoRepositorio ?? (_empleadoRepositorio = new EmpleadoRepositorio(_context));

        // ====================================================================================================================================== //

        public IRepositorio<Pago> _pagoRepositorio;

        public IRepositorio<Pago> PagoRepositorio => _pagoRepositorio ?? (_pagoRepositorio = new Repositorio<Pago>(_context));

        // ====================================================================================================================================== //

        public IRepositorio<Cuota> _cuotaRepositorio;

        public IRepositorio<Cuota> CuotaRepositorio => _cuotaRepositorio ?? (_cuotaRepositorio = new Repositorio<Cuota>(_context));

        // ====================================================================================================================================== //

        public IRepositorio<PrecioCarrera> _precioCarreraRepositorio;

        public IRepositorio<PrecioCarrera> PrecioCarreraRepositorio => _precioCarreraRepositorio ?? (_precioCarreraRepositorio = new Repositorio<PrecioCarrera>(_context));

        // ====================================================================================================================================== //

        public IRepositorio<Usuario> _usuarioRepositorio;

        public IRepositorio<Usuario> UsuarioRepositorio => _usuarioRepositorio ?? (_usuarioRepositorio = new Repositorio<Usuario>(_context));

        // ====================================================================================================================================== //

        public IRepositorio<Ciudad> _ciudadRepositorio;

        public IRepositorio<Ciudad> CiudadRepositorio => _ciudadRepositorio ?? (_ciudadRepositorio = new Repositorio<Ciudad>(_context));

        // ====================================================================================================================================== //

        public IRepositorio<Extension> _extensionRepositorio;

        public IRepositorio<Extension> ExtensionRepositorio => _extensionRepositorio ?? (_extensionRepositorio = new Repositorio<Extension>(_context));

        // ====================================================================================================================================== //

        public ICargaMasivaRepositorio<Alumno> _cargaMasivaAlumnoRepositorio;

        public ICargaMasivaRepositorio<Alumno> CargaMasivaAlumnoRepositorio => _cargaMasivaAlumnoRepositorio ?? (_cargaMasivaAlumnoRepositorio = new CargaMasivaRepositorio<Alumno>(_context));

        // ====================================================================================================================================== //

        public ICargaMasivaRepositorio<Persona> _cargaMasivaPersonaRepositorio;

        public ICargaMasivaRepositorio<Persona> CargaMasivaPersonaRepositorio => _cargaMasivaPersonaRepositorio ?? (_cargaMasivaPersonaRepositorio = new CargaMasivaRepositorio<Persona>(_context));

        // ============================================================================================================ //
        private IRepositorio<Contador> contadorRepositorio;

        public IRepositorio<Contador> ContadorRepositorio => contadorRepositorio ?? (contadorRepositorio = new Repositorio<Contador>(_context));

    }
}

