using Aplicacion.Constantes.Enums;
using IServicios.Base.Base_DTO;
using IServicios.Pago.PagoDTO;
using IServicios.Persona.DTO_s;
using IServicios.PrecioCuota.PrecioCuotaDTO;

namespace IServicios.Cuota.CuotaDTO
{
    public class CuotaDTO : BaseDTO
    {

        public CuotaDTO()
        {
            if (Pagos == null)
                Pagos = new List<PagoDTO>();
        }

        public int Numero { get; set; }

        public decimal MontoAbonado { get; set; }

        public DateTime Fecha { get; set; }

        public EstadoCuota EstadoCuota { get; set; }

        public string EstadoCuotaStr => EstadoCuota == EstadoCuota.Pagada ? "PAGADA" : "PENDIENTE";

        public long AlumnoId { get; set; }

        public AlumnoDTO? Alumno { get; set; }

        public long PrecioCuotaId { get; set; }

        public PrecioCuotaDTO? PrecioCuota { get; set; }

        public int NumeroDePagos { get; set; }

        public List<PagoDTO> Pagos { get; set; }

    }
}
