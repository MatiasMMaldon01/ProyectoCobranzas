using Aplicacion.Constantes.Enums;
using IServicios.Base.Base_DTO;

namespace IServicios.Persona.DTO_s
{
    public class PersonaDTO : BaseDTO
    {
        public string Apynom { get; set; }

        public TipoDocumento TipoDoc { get; set; }

        public string NroDoc { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

        public int ExtensionId { get; set; }

        public string Extension { get; set; }

        public int CiudadId { get; set; }

        public int CodigoPostal { get; set; }

        public string Ciudad { get; set; }

    }
}
