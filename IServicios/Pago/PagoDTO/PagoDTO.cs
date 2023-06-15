using IServicios.Base.Base_DTO;
using IServicios.Persona.DTO_s;

namespace IServicios.Pago.PagoDTO
{
    public class PagoDTO : BaseDTO
    {
        public decimal Monto { get; set; }

        public long NroRecibo { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaRecibo { get; set; }

        public int CuotaId { get; set; }

        public int AlumnoId { get; set; }

        public AlumnoDTO Alumno { get; set; }

    }
}
