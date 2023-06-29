namespace IServicios.Persona.CargasMasivas
{
    public interface IAlumnoCargaMasiva
    {
        Task CargaMasivaAlumno();
        Task EliminacionMasivaAlumnos(DateTime desde, DateTime hasta);
    }
}
