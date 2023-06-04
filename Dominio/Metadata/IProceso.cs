using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Metadata
{
    public interface IProceso
    {
        [Display(Name = @"Código")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        int Codigo { get; set; }

        [Display(Name = @"Descripción")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Descricpion { get; set; }

        [Display(Name = @"EntidadMovimiento")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string EntidadMovimiento { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Usuario")]
        int UsuarioId { get; set; }

    }
}
