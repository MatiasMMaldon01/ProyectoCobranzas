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

        public long NroRecibo { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaRecibo { get; set; }

        public int CuotaId { get; set; }

        public int AlumnoId { get; set; }


        // Propiedades de Navegacion
        public virtual Cuota Cuota { get; set; }

        public virtual Alumno Alumnno { get; set; }

    }
}
