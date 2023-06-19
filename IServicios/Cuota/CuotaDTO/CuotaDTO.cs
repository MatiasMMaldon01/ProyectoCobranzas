using IServicios.Base.Base_DTO;
using IServicios.Pago.PagoDTO;
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

        public decimal MontoCuota { get; set; }

        public DateTime Fecha { get; set; }

        public int PrecioCarreraId { get; set; }

        public PrecioCarreraDTO? PrecioCarrera { get; set; }

        public int NumeroDePagos { get; set; }

        public List<PagoDTO> Pagos { get; set; }

    }
}
