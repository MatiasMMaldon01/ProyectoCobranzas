using Dominio.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Extension")]
    [MetadataType(typeof(IExtension))]
    public class Extension : EntidadBase
    {
        public string Descripcion { get; set; }


        public ICollection<Persona> Personas { get; set; }
    }
}
