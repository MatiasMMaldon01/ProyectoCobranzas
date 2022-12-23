using Dominio.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Pago")]
    [MetadataType(typeof(IPago))]
    public class Pago : EntidadBase
    {

        public decimal Monto { get; set; }

        public long CuotaId { get; set; }


        // Propiedades de Navegacion
        public virtual Cuota Cuota { get; set; }
    }
}
