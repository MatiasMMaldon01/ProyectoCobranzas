using Dominio.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    [Table("Persona_Empleado")]
    [MetadataType(typeof(IEmpleado))]
    public class Empleado : Persona
    {
        public string AreaTrabajo { get; set; }
    }
}
