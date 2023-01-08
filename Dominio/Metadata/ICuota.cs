using Aplicacion.Constantes.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Metadata
{
    public  interface ICuota
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        int Numero { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        decimal MontoCuota { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        decimal MontoAbonado { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DataType(DataType.DateTime)]
        DateTime Fecha { get; set; }

        EstadoCuota EstadoCuota { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Alumno")]
        long AlumnoId { get; set; }

    }
}
