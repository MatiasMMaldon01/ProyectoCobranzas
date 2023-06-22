using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Aplicacion.Constantes.Enums;

namespace Dominio.Metadata
{
    public interface IContador
    {

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [EnumDataType(typeof(Entidad))]
        Entidad Entidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [DefaultValue(0)]
        int Valor { get; set; }
    }
}
