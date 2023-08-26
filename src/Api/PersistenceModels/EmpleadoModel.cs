using Aplicacion.Constantes.Enums;

namespace Api.PersistenceModels
{
    public class EmpleadoModel : BaseModel
    {

        public string Apynom { get; set; }

        public TipoDocumento TipoDoc { get; set; }

        public string NroDoc { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

        public string AreaTrabajo { get; set; }
        public int CiudadId { get; set; }
        public int ExtensionId { get; set; }
    }
}
