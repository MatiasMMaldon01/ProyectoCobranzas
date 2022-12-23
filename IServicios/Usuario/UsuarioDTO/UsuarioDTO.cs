using Aplicacion.Constantes;
using IServicios.Base.Base_DTO;

namespace IServicios.Usuario.UsuarioDTO
{
    public class UsuarioDTO : BaseDTO
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }
    }
}
