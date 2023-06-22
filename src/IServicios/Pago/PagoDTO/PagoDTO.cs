using IServicios.Base.Base_DTO;
using IServicios.Persona.DTO_s;

namespace IServicios.Pago.PagoDTO
{
    public class PagoDTO : BaseDTO
    {
        public string Legajo { get; set; }

        public int CantCuota { get; set; }

        public int NroCuota { get; set; } 

        public decimal Monto { get; set; }

        public long NroRecibo { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaRecibo { get; set; }

        public int AlumnoId { get; set; }

        public AlumnoDTO Alumno { get; set; }

    }
}
