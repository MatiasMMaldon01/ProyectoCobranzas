using Dominio.Interfaces;
using IServicios.Persona;

namespace Servicios.PersonaServicio
{
    public class EmpleadoServicio : PersonaServicio, IEmpleadoServicio
    {
        public EmpleadoServicio(IUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo)
        {
        }
    }
}
