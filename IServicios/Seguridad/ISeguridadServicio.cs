using IServicios.Usuario.UsuarioDTO;

namespace IServicios.Seguridad
{
    public interface ISeguridadServicio
    {
        Task<UsuarioDTO> ValidarUsuario(string nombreUsuario, string password);

        string CrearToken(UsuarioDTO usuarioDto);

    }
}
