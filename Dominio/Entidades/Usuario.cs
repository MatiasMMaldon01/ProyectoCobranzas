using Dominio.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Aplicacion.Constantes.Enums;

namespace Dominio.Entidades
{
    [Table("Usuario")]
    [MetadataType(typeof(IUsuario))]
    public  class Usuario : EntidadBase
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }
        public long PersonaId { get; set; }

        // Propiedades de Navegacion
        public virtual Persona Persona { get; set; }

    }
}
