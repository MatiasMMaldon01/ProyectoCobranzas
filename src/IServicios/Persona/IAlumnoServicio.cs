using IServicios.Persona.DTO_s;

namespace IServicios.Persona
{
    public interface IAlumnoServicio : IPersonaServicio
    {
        Task<(int, int)> ObtenerNroCuotasYId(string legajo);

        Task<IEnumerable<FiltroAlumnosDTO>> FiltrarAlumnos(int corte);
    }
}
