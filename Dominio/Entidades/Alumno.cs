using Dominio.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{

    [Table("Alumno")]
    [MetadataType(typeof(IAlumno))]
    public class Alumno : EntidadBase
    {
        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string Dni { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

        public long CarreraId { get; set; }

        public long UsuarioId { get; set; }



        // Propiedades de Navegacion
        public virtual Carrera Carrera { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Cuota> Cuotas { get; set; }
    }
}
