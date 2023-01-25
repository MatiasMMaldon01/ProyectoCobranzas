using IServicios.Base.Base_DTO;

namespace IServicios.PrecioCuota.PrecioCuotaDTO
{
    public class PrecioCuotaDTO : BaseDTO
    {
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public long CarreraId { get; set; }

        public string Carrera { get; set; }
    }
}
