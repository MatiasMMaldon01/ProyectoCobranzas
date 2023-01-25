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

        public decimal MontoAbonado { get; set; }

        public DateTime Fecha { get; set; }

        public EstadoCuota EstadoCuota { get; set; }

        public long AlumnoId { get; set; }

        public long PrecioCuotaId { get; set; }


        //Propiedades de Navegacion

        public virtual Alumno Alumno { get; set; }

        public virtual PrecioCuota PrecioCuota { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }


    }
}
