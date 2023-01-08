using System.ComponentModel.DataAnnotations;

namespace Dominio.Metadata
{
    public interface IEmpleado: IPersona
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "AreaTrabajo")]
        string AreaTrabajo { get; set; }
    }
}
