using IServicios.Base.Base_DTO;
using IServicios.Cuota.CuotaDTO;
using System.Diagnostics.CodeAnalysis;

namespace IServicios.Pago.PagoDTO
{
    public class PagoDTO : BaseDTO
    {
        public decimal Monto { get; set; }
        
        public DateTime FechaPago { get; set; }

        public decimal PorcPago { get; set; }

        public long CuotaId { get; set; }

    }
}
