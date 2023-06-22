using System.ComponentModel.DataAnnotations;

namespace Dominio.Metadata
{
    public interface IExtension
    {
        [Display(Name = @"Descripción")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Descripcion { get; set; }
    }
}
