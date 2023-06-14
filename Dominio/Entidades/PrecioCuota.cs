using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Dominio.Metadata;

namespace Dominio.Entidades
{
    [Table("PrecioCarrera")]
    [MetadataType(typeof(IPrecioCarrera))]
    public class PrecioCuota : EntidadBase
    {

        public decimal Monto { get; set; }

        public decimal Matricula { get; set; }

        public DateTime Fecha { get; set; }

        public int CarreraId { get; set; } 

        // Propiedades de Navegacion
        public virtual Carrera Carrera { get; set; }

        public virtual ICollection<Cuota> Cuotas { get; set; }

    }
}
