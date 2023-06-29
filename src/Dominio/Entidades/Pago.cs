using Dominio.Metadata;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Pago")]
    [MetadataType(typeof(IPago))]
    [Index(nameof(Legajo), nameof(NroRecibo))]
    public class Pago : EntidadBase
    {
        public string Legajo { get; set; }

        public int NroCuota { get; set; }

        public decimal Monto { get; set; }

        public long NroRecibo { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaRecibo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int AlumnoId { get; set; }


        // Propiedades de Navegacion

        public virtual Alumno Alumno { get; set; }

    }
}
