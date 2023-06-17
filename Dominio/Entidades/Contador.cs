using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Dominio.Metadata;
using Aplicacion.Constantes.Enums;

namespace Dominio.Entidades
{
    [Table("Contador")]
    [MetadataType(typeof(IContador))]
    public class Contador : EntidadBase
    {
        public Entidad Entidad { get; set; }

        public int Valor { get; set; }
    }
}
