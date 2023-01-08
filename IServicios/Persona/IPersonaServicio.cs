using IServicios.Persona.DTO_s;

namespace IServicios.Persona
{
    public interface IPersonaServicio
    {
        Task<long> Crear(PersonaDTO persona);

        Task Modificar(PersonaDTO persona);

        Task Eliminar(Type tipoEntidad, long id);

        Task<PersonaDTO> Obtener(Type tipoEntidad, long id);

        Task<IEnumerable<PersonaDTO>> Obtener(Type tipoEntidad, string cadenaBuscar, bool mostrarTodos);

        Task<IEnumerable<PersonaDTO>> ObtenerTodos(Type tipoEntidad);


        // ==================================================================== //

        void AgregarOpcionDiccionario(Type type, string value);
    }
}
