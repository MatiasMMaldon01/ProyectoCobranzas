using Dominio.Interfaces;
using IServicios.Persona;
using IServicios.Persona.DTO_s;
using Servicios.Base;

namespace Servicios.PersonaServicio
{
    public class PersonaServicio : IPersonaServicio
    {
        protected readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private Dictionary<Type, string> _diccionario;

        public PersonaServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _diccionario = new Dictionary<Type, string>();

            InicializadorDiccionario();
        }

        private void InicializadorDiccionario()
        {
            _diccionario.Add(typeof(EmpleadoDTO), "Servicios.PersonaServicio.Empleado");
            _diccionario.Add(typeof(AlumnoDTO), "Servicios.PersonaServicio.Alumno");
        }

        public void AgregarOpcionDiccionario(Type type, string value)
        {
            _diccionario.Add(type, value);
        }

        public async Task<long> Crear(PersonaDTO entidad)
        {
            var persona =  GenericInstance<Persona>.InstanciarEntidad(entidad, _diccionario, _unidadDeTrabajo);
            var id = await persona.Crear(entidad);
            return id;
        }

        public async Task Eliminar(Type tipoEntidad, long id)
        {
            var persona = GenericInstance<Persona>.InstanciarEntidad(tipoEntidad, _diccionario, _unidadDeTrabajo);
            await persona.Eliminar(id);
        }

        public async Task<IEnumerable<PersonaDTO>> Obtener(Type tipo, string cadenaBuscar, bool mostrarTodos = false)
        {
            var persona = GenericInstance<Persona>.InstanciarEntidad(tipo, _diccionario, _unidadDeTrabajo);

            return await persona.Obtener(cadenaBuscar, mostrarTodos);
        }

        public async Task<PersonaDTO> Obtener(Type tipo, long id)
        {
            var persona = GenericInstance<Persona>.InstanciarEntidad(tipo, _diccionario, _unidadDeTrabajo);

            return  await persona.Obtener(id);
        }

        public async Task<IEnumerable<PersonaDTO>> ObtenerTodos(Type tipo, bool mostrarTodos = false)
        {
            var persona = GenericInstance<Persona>.InstanciarEntidad(tipo, _diccionario, _unidadDeTrabajo);

            return await persona.ObtenerTodos();
        }

        public async Task Modificar(PersonaDTO entidad)
        {
            var persona = GenericInstance<Persona>.InstanciarEntidad(entidad, _diccionario, _unidadDeTrabajo);

            await persona.Modificar(entidad);
        }
    }
}
