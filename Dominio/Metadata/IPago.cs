using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Metadata
{
    public interface IPago
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = @"NroCuota")]
        int NroCuota { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        decimal Monto { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long NroRecibo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime FechaRecibo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime FechaCarga { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = @"Alumno")]
        int AlumnoId { get; set; }
    }
}
