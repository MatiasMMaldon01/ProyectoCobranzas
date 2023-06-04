using IServicios.Base.Base_DTO;
using IServicios.Carrera.Carrera_DTO;
using IServicios.Persona.DTO_s;

namespace IServicios.AlumnoCarrera.AlumnoCarreraDTO
{
    public class AlumnoCarreraDTO : BaseDTO
    {
        public int CarreraId { get; set; }

        public CarreraDto Carrera { get; set; }

        public int AlumnoId { get; set; }

        public AlumnoDTO Alumno { get; set; }
    }
}
