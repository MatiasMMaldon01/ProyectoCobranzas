using IServicios.Base.Base_DTO;

namespace IServicios.Persona.DTO_s
{
    public class PersonaDTO : BaseDTO
    {
        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string Dni { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

    }
}
