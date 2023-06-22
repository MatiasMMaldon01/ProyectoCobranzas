using IServicios.Base.Base_DTO;

namespace IServicios.PrecioCuota.PrecioCuotaDTO
{
    public class PrecioCarreraDTO : BaseDTO
    {
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Matricula { get; set; }

        public int CarreraId { get; set; }

        public string Carrera { get; set; }
    }
}
