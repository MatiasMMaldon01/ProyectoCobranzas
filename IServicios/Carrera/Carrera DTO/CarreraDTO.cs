using IServicios.Base.Base_DTO;

namespace IServicios.Carrera.Carrera_DTO
{
    public class CarreraDto : BaseDTO
    {
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }
        
        public decimal CantCuotas { get; set; }

        public decimal PrecioCuo { get; set; }
    }
}
