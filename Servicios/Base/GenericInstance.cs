using IServicios.Base.Base_DTO;

namespace Servicios.Base
{
    public class GenericInstance<T> where T : class
    {
        public static T InstanciarEntidad(string tipoEntidad)
        {
            var tipoObjeto = Type.GetType(tipoEntidad);
            if (tipoObjeto == null) return null;
            var entidad = Activator.CreateInstance(tipoObjeto) as T;
            return entidad;
        }

        public static T InstanciarEntidad(BaseDTO entidad, Dictionary<Type, string> _diccionario)
        {
            if (!_diccionario.TryGetValue(entidad.GetType(), out var tipoEntidad))
                throw new Exception($"No hay {entidad.GetType()} para Instanciar.");

            var T = InstanciarEntidad(tipoEntidad);
            if (T == null) throw new Exception($"Ocurrió un error al Instanciar {entidad.GetType()}");
            return T;
        }

        public static T InstanciarEntidad(Type tipo, Dictionary<Type, string> _diccionario)
        {
            if (!_diccionario.TryGetValue(tipo, out var tipoEntidad))
                throw new Exception($"No hay {tipoEntidad} para Instanciar.");
            var T = InstanciarEntidad(tipoEntidad);
            if (T == null) throw new Exception($"Ocurrió un error al Instanciar {tipo}");
            return T;
        }
    }
}
