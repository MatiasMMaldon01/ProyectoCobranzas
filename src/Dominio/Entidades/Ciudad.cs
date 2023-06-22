using Dominio.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Ciudad")]
    [MetadataType(typeof(ICiudad))]
    public class Ciudad : EntidadBase
    {
        public string Descripcion { get; set; }


        public ICollection<Persona> Personas { get; set; }
    }
}
