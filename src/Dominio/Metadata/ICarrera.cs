using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Metadata
{
    public interface ICarrera
    {
        [Display(Name = @"Descripción")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        decimal CantidadCuotas { get; set; }
    }
}
