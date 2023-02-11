using System.ComponentModel.DataAnnotations;

namespace Dominio.Metadata
{
    public  interface IAlumno : IPersona
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Carrera")]
        int Legajo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime FechaIngreso { get; set; }
    }
}
