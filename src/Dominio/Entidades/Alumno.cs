using Dominio.Metadata;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{

    [Table("Persona_Alumno")]
    [MetadataType(typeof(IAlumno))]
    [Index(nameof(Legajo))]
    public class Alumno : Persona
    {
        public string Legajo { get; set; }

        public DateTime FechaIngreso { get; set; }

        public decimal PorcBeca { get; set; }

        public int CarreraId { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Propiedades de Navegacion
        public virtual ICollection<Pago> Pagos { get; set; }

        public virtual Carrera Carrera { get; set; }
    }
}
