using Aplicacion.Constantes.Enums;

namespace Api.PersistenceModels
{
    public class UsuarioModel : BaseModel
    {

        public string Nombre { get; set; }

        public string Password { get; set; }

        public Rol Rol { get; set; }

        public long PersonaId { get; set; }

    }
}
