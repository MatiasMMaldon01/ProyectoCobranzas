using Dominio.Interfaces;
using IServicios.Persona;

namespace Servicios.PersonaServicio
{
    public class AlumnoServicio : PersonaServicio, IAlumnoServicio
    {
        public AlumnoServicio(IUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo)
        {
        }
    }
}
