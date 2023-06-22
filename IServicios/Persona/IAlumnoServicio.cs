namespace IServicios.Persona
{
    public interface IAlumnoServicio : IPersonaServicio
    {
        Task<(int, int)> ObtenerNroCuotasYId(string legajo);
    }
}
