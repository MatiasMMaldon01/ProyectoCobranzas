namespace IServicios.Persona.DTO_s
{
    public class AlumnoDTO : PersonaDTO
    {
        public int Legajo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public long CarreraId { get; set; }
        public string Carrera { get; set; }
    }
}
