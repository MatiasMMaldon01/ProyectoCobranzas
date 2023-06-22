using Aplicacion.Constantes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Metadata
{
    public interface IPersona
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Apynom")]
        [StringLength(500, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Apynom { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        TipoDocumento TipoDoc { get; set; }

        [Display(Name = "NroDocumento")]
        [StringLength(8, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string NroDoc { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Dirección")]
        [StringLength(400, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(25, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "E-Mail")]
        [StringLength(250, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El campo {0} no tiene el formato correcto")]
        string Mail { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Extension")]
        int ExtensionId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Ciudad")]
        int CiudadId { get; set; }

    }
}
