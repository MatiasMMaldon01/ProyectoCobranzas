using Aplicacion.Constantes.Enums;
using IServicios.Base.Base_DTO;
using IServicios.Persona.DTO_s;

namespace IServicios.Usuario.UsuarioDTO
{
    public class UsuarioDTO : BaseDTO
    {
        public string Nombre { get; set; }

        public string Password { get; set; }

        public Rol Rol { get; set; }

        public string RolStr { get; set; }

        public int PersonaId { get; set; }

        public PersonaDTO Persona { get; set; }
    }
}
