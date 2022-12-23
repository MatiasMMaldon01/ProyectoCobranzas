using Aplicacion.Constantes;
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

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public Rol Rol { get; set; }

        public long AlumnoId { get; set; }


        //Propiedades de Navegacion

        public virtual Alumno Alumno { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }


    }
}
