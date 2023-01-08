using Dominio.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    [Table("Persona")]
    [MetadataType(typeof(IPersona))]
    public class Persona : EntidadBase
    {
        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string Dni { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

    }
}
