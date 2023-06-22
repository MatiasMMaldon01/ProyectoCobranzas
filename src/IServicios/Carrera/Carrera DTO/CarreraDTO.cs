using IServicios.Base.Base_DTO;

namespace IServicios.Carrera.Carrera_DTO
{
    public class CarreraDto : BaseDTO
    {
        public string Descripcion { get; set; } = "Carrera desconocida";

        public DateTime Fecha { get; set; }
        
        public decimal CantCuotas { get; set; }

        public decimal PrecioCarrera { get; set; }
    }
}
