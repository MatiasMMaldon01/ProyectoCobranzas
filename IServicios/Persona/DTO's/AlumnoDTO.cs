using IServicios.Carrera.Carrera_DTO;
using IServicios.Pago.PagoDTO;

namespace IServicios.Persona.DTO_s
{
    public class AlumnoDTO : PersonaDTO
    {
        public AlumnoDTO()
        {
            if (Pagos == null)
                Pagos = new List<PagoDTO>();
        }

        public string Legajo { get; set; }

        public DateTime FechaIngreso { get; set; }

        public List<PagoDTO> Pagos { get; set; }

    }
}
