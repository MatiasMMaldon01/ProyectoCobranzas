using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Pago;

namespace Servicios.PagoServicio
{
    public class PagoServicio : IPagoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public PagoServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public Task<int> Crear(BaseDTO dtoEntidad)
        {
            throw new NotImplementedException();
        }

        public Task Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task Modificar(BaseDTO dtoEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<BaseDTO> Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseDTO>> ObtenerPorAlumnoId(int alumnoId, bool mostrarTodos = true)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {
            throw new NotImplementedException();
        }
    }
    
}
