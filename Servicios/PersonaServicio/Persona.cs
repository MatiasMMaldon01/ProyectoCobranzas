using Dominio.Interfaces;
using IServicios.Persona.DTO_s;

namespace Servicios.PersonaServicio
{
    public class Persona
    {
        protected readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public Persona(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public virtual Task<long> Crear(PersonaDTO entidad)
        {
            return null;
        }

        public virtual Task Eliminar(long id)
        {
            return null;
        }

        public virtual Task<IEnumerable<PersonaDTO>> Obtener(string cadenaBuscar, bool mostrarTodos)
        {
            return null;
        }

        public virtual Task<PersonaDTO> Obtener(long id)
        {
            return null;
        }

        public virtual Task Modificar(PersonaDTO entidad)
        {
            return null;
        }

        public virtual Task<IEnumerable<PersonaDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            return null;
        }

    }
}
