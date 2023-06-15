using Aplicacion.Constantes.Enums;

namespace Api.PersistenceModels
{
    public class AlumnoModel : BaseModel
    {
        public string Legajo { get; set; }

        public string Apynom { get; set; }

        public TipoDocumento TipoDoc { get; set; }

        public string NroDoc { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

        public DateTime FechaIngreso { get; set; }

        public int ExtensionId { get; set; }

        public int CiudadId { get; set; }

    }
}
