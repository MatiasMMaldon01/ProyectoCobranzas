using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Metadata
{
    public interface IPago
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        decimal Monto { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        decimal PorcPago { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = @"Cuota")]
        int CuotaId { get; set; }
    }
}
