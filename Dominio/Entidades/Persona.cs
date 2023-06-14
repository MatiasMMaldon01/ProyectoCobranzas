using Dominio.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Aplicacion.Constantes.Enums;

namespace Dominio.Entidades
{
    [Table("Persona")]
    [MetadataType(typeof(IPersona))]
    public class Persona : EntidadBase
    {
        public string Apynom { get; set; }

        public TipoDocumento TipoDoc { get; set; }

        public string NroDoc { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

        public int ExtensionId { get; set; }

        public int CiudadId { get; set; }


        public Ciudad Ciudad { get; set; }

        public Extension Extension { get; set; }

    }
}
