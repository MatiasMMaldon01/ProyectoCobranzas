using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Metadata
{
    public interface IPrecioCarrera
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name =@"Carrera")]
        int CarreraId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        decimal Monto { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        decimal Matricula { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime Fecha { get; set; }
    }
}
