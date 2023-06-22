using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Usuario;
using Servicios.Base;
using System.Linq.Expressions;
using IServicios.Usuario.UsuarioDTO;
using Aplicacion.Constantes;
using IServicios.Persona.DTO_s;

namespace Servicios.UsuarioServicio
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public UsuarioServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Eliminar(int id)
        {
            await _unidadDeTrabajo.UsuarioRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<int> Crear(BaseDTO dtoEntidad)
        {

            var dto = (UsuarioDTO)dtoEntidad;

            var entidad = new Usuario
            {
                Nombre = dto.Nombre,
                Password = EncriptarPassword.GetSHA256(dto.Password),
                Rol = dto.Rol,
                PersonaId = dto.PersonaId,
                EstaEliminado = false,
            };

            await _unidadDeTrabajo.UsuarioRepositorio.Crear(entidad);

            _unidadDeTrabajo.Commit();

            return entidad.Id;
        }

        public async Task Modificar(BaseDTO dtoEntidad)
        {

            var dto = (UsuarioDTO)dtoEntidad;

            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("No se encotró la Usuario que quiere modificar");

            entidad.Nombre = dto.Nombre;
            entidad.Password = EncriptarPassword.GetSHA256(dto.Password);
            entidad.Rol = dto.Rol;
            entidad.PersonaId = dto.PersonaId;

            await _unidadDeTrabajo.UsuarioRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(int id)
        {
            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.Obtener(id, "Persona");

            if (entidad == null) throw new Exception("No se encotró la Usuario que esta buscando");

            return new UsuarioDTO
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Password = entidad.Password,
                Rol = entidad.Rol,
                RolStr = entidad.Rol.ToString(),
                PersonaId = entidad.PersonaId,
                Persona = new PersonaDTO
                {
                    Id = entidad.Persona.Id,
                    Apynom = entidad.Persona.Apynom,
                    TipoDoc = entidad.Persona.TipoDoc,
                    NroDoc = entidad.Persona.NroDoc,
                    Direccion = entidad.Persona.Direccion,
                    Telefono = entidad.Persona.Telefono,
                    Mail = entidad.Persona.Mail
                },
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos(bool mostrarTodos = false)
        {

            Expression<Func<Usuario, bool>> filtro = x => x.EstaEliminado;

            if (!mostrarTodos)
            {
                filtro = x => !x.EstaEliminado;
            }

            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerTodos(filtro, "Persona");

            return entidad.Select(x => new UsuarioDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Password = x.Password,
                Rol = x.Rol,
                RolStr = x.Rol.ToString(),
                PersonaId = x.PersonaId,
                Persona = new PersonaDTO {
                    Id = x.Persona.Id,
                    Apynom = x.Persona.Apynom,
                    TipoDoc = x.Persona.TipoDoc,
                    NroDoc = x.Persona.NroDoc,
                    Direccion = x.Persona.Direccion,
                    Telefono = x.Persona.Telefono,
                    Mail = x.Persona.Mail
                },
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Nombre)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = false)
        {
            Expression<Func<Usuario, bool>> filtro = x => x.Nombre.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.Obtener(filtro, "Persona");

            return entidad.Select(x => new UsuarioDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Password = x.Password,
                Rol = x.Rol,
                RolStr = x.Rol.ToString(),
                PersonaId = x.PersonaId,
                Persona = new PersonaDTO
                {
                    Id = x.Persona.Id,
                    Apynom = x.Persona.Apynom,
                    TipoDoc = x.Persona.TipoDoc,
                    NroDoc = x.Persona.NroDoc,
                    Direccion = x.Persona.Direccion,
                    Telefono = x.Persona.Telefono,
                    Mail = x.Persona.Mail
                },
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Nombre)
                .ToList();
        }

    }
}
