using Aplicacion.Constantes.Enums;
using Dominio.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Cuota")]
    [MetadataType(typeof(ICuota))]
    public class Cuota : EntidadBase
    {
        public int Numero { get; set; }

        public decimal MontoCuota { get; set; }

        public DateTime Fecha { get; set; }

        public EstadoCuota EstadoCuota { get; set; }

        public int PrecioCuotaId { get; set; }


        //Propiedades de Navegacion


        public virtual PrecioCuota PrecioCuota { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }


    }
}
