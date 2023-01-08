using Dominio.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Dominio.Entidades
{
    [Table("Proceso")]
    [MetadataType(typeof(IProceso))]
    public class Proceso : EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string EntidadMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public long UsuarioId { get; set; }


        // Propiedades de Navegacion

        public virtual Usuario Usuario { get; set; }


        
    }
}
