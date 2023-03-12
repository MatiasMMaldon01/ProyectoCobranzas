using IServicios.Carrera.Carrera_DTO;

namespace IServicios.Persona.DTO_s
{
    public class AlumnoDTO : PersonaDTO
    {
        public AlumnoDTO()
        {
            if (Carreras == null)
                Carreras = new List<CarreraDto>();
        }

        public int Legajo { get; set; }
        public decimal PorcBeca { get; set; }
        public DateTime FechaIngreso { get; set; }
        public List<long>? AlumnoCarreraId { get; set; }
        public List<CarreraDto> Carreras { get; set; }

    }
}
