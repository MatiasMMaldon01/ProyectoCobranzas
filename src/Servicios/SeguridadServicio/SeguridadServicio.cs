using Dominio.Interfaces;
using IServicios.Seguridad;
using IServicios.Usuario.UsuarioDTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion.Constantes;
using Microsoft.AspNetCore.Http.Headers;

namespace Servicios.SeguridadServicio
{
    public class SeguridadServicio : ISeguridadServicio
    {
        private readonly IConfiguration _configuration;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public SeguridadServicio(IConfiguration configuration, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _configuration = configuration;
            _unidadDeTrabajo = unidadDeTrabajo;
        }


        public async Task<UsuarioDTO> ValidarUsuario(string nombreUsuario, string password)
        {
            var usuario = await  _unidadDeTrabajo.UsuarioRepositorio.Obtener(x => x.Nombre == nombreUsuario && x.Password == EncriptarPassword.GetSHA256(password));
            var usuarioRes = usuario.FirstOrDefault();

            if (usuarioRes == null)
                throw new Exception("El usuario no existe");

            var respuesta = new UsuarioDTO
            {
                Id = usuarioRes.Id,
                Nombre = usuarioRes.Nombre,
                Password = usuarioRes.Password,
                Rol = usuarioRes.Rol,
                Eliminado = usuarioRes.EstaEliminado
            };

            return respuesta;

        }

        public string CrearToken(UsuarioDTO usuarioDTO)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioDTO.Nombre),
                new Claim(ClaimTypes.Role, usuarioDTO.Rol.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        
    }
}
