using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Metadata
{
    public  interface IAlumno : IPersona
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Legajo")]
        int Legajo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime FechaCreacion { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        decimal PorcBeca { get; set; }
    }
}
