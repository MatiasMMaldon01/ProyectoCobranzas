using IServicios.Base.Base_DTO;

namespace IServicios.Carrera.Carrera_DTO
{
    public class CarreraDto : BaseDTO
    {
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }
        
<<<<<<< HEAD
        public decimal CantCuotas { get; set; }
=======
        public decimal cantCuotas { get; set; }
>>>>>>> c93c81a7e39c87ed3f0991f6d7efee3457d96202

        public decimal precioCuo { get; set; }
    }
}
