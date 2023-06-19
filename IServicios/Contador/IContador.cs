using Aplicacion.Constantes.Enums;

namespace IServicios.Contador
{
    public interface IContadorServicio
    {
        Task<int> ObtenerSiguienteNumero(Entidad entidad);

        Task CargarNumero(Entidad entidad, int valor);
    }
}