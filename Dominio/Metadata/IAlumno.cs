using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Dominio.Metadata
{
    public  interface IAlumno
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Apellido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Nombre")]
        [StringLength(200, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Nombre { get; set; }

        [Display(Name = "DNI")]
        [StringLength(8, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Dni { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Dirección")]
        [StringLength(400, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(25, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "E-Mail")]
        [StringLength(250, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El campo {0} no tiene el formato correcto")]
        string Mail { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Usuario")]
        long UsuarioId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Carrera")]
        long CarreraId { get; set; }
    }
}
