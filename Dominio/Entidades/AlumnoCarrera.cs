using Dominio.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    [Table("AlumnoCarrera")]
    [MetadataType(typeof(IAlumnoCarrera))]
    public class AlumnoCarrera : EntidadBase
    {
        public long AlumnoId { get; set; }

        public long CarreraId { get; set; }

        // Propiedades De Navegacion

        public virtual Carrera Carrera { get; set; }

        public virtual Alumno Alumno { get; set; }
    }
}
