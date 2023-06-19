using Aplicacion.Constantes.Enums;
using Dominio.Interfaces;
using IServicios.Contador;

namespace Servicios.Contador
{
    public class ContadorServicio : IContadorServicio
    {
        private IUnidadDeTrabajo _unidadDeTrabajo;
        public ContadorServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task CargarNumero(Entidad entidad, int valor)
        {

            var lista = await _unidadDeTrabajo.ContadorRepositorio.Obtener(x => x.Entidad == entidad);

            var resultado = lista.FirstOrDefault();

            if (resultado == null)
            {
                throw new Exception("Ocurrio un error al Obtener el numero");
            }

            resultado.Valor = valor;

            await _unidadDeTrabajo.ContadorRepositorio.Modificar(resultado);
            _unidadDeTrabajo.Commit();
        }

        public async Task<int> ObtenerSiguienteNumero(Entidad entidad)
        {
            var lista = await _unidadDeTrabajo.ContadorRepositorio.Obtener(x => x.Entidad == entidad);

            var resultado = lista.FirstOrDefault();



            if (resultado == null)
            {
                throw new Exception("Ocurrio un error al Obtener el numero");
            }

            return resultado.Valor + 1;
        }
    }
}