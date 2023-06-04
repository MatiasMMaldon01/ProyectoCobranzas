using System.ComponentModel.DataAnnotations;

namespace Dominio.Metadata
{
    public interface IAlumnoCarrera
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = @"Alumno")]
        int AlumnoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = @"Carrera")]
        int CarreraId { get; set; }
    }
}
