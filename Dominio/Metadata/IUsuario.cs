using Aplicacion.Constantes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Metadata
{
    public  interface IUsuario
    {

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Persona")]
        long PersonaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = @"Nombre Usuario")]
        [StringLength(50, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = @"Contraseña")]
        [StringLength(400, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        [DataType(DataType.Password)]
        string Password { get; set; }

        Rol Rol { get; set; }
    }
}
