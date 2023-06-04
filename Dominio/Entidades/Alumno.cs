using Dominio.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{

    [Table("Persona_Alumno")]
    [MetadataType(typeof(IAlumno))]
    public class Alumno : Persona
    {
        public string Legajo { get; set; }

        public DateTime FechaIngreso { get; set; }

        public decimal PorcBeca { get; set; }

        // Propiedades de Navegacion
        public virtual ICollection<AlumnoCarrera> AlumnoCarreras { get; set; }
        public virtual ICollection<Cuota> Cuotas { get; set; }
    }
}
