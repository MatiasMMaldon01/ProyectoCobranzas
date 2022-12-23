using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Dominio.Entidades
{
    public class EntidadBase
    {
        [Key] // AutoIncremental - Unico
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")] // EL campo sea Obligatorio.
        [Display(Name = "esta Eliminado")] // Nombre que se va a mostrar
        [DefaultValue(0)] // False = 0; True = 1
        public bool EstaEliminado { get; set; }
    }
}
