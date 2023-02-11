using IServicios.Base.Base_DTO;
using IServicios.Carrera.Carrera_DTO;
using IServicios.Persona.DTO_s;

namespace IServicios.AlumnoCarrera.AlumnoCarreraDTO
{
    public class AlumnoCarreraDTO : BaseDTO
    {
        public long CarreraId { get; set; }

        public CarreraDto Carrera { get; set; }

        public long AlumnoId { get; set; }

        public AlumnoDTO Alumno { get; set; }
    }
}
