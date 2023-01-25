using Aplicacion.Constantes.Enums;
using IServicios.Base.Base_DTO;
using IServicios.Persona.DTO_s;
using IServicios.PrecioCuota.PrecioCuotaDTO;

namespace IServicios.Cuota.CuotaDTO
{
    public class CuotaDTO : BaseDTO
    {
        public int Numero { get; set; }

        public decimal MontoCuota { get; set; }

        public decimal MontoAbonado { get; set; }

        public DateTime Fecha { get; set; }

        public EstadoCuota EstadoCuota { get; set; }

        public long AlumnoId { get; set; }

        public AlumnoDTO Alumno { get; set; }

        public long PrecioCuotaId { get; set; }

        public PrecioCuotaDTO PrecioCuota { get; set; }

    }
}
