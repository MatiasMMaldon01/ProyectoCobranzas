﻿using Dominio.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{

    [Table("Persona_Alumno")]
    [MetadataType(typeof(IAlumno))]
    public class Alumno : Persona
    {
        public int Legajo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public long CarreraId { get; set; }

        // Propiedades de Navegacion
        public virtual Carrera Carrera { get; set; }
        public virtual ICollection<Cuota> Cuotas { get; set; }
    }
}
