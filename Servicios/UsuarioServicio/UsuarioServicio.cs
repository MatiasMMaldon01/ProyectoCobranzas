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

        public async Task Eliminar(long id)
        {
            await _unidadDeTrabajo.UsuarioRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public async Task<long> Crear(BaseDTO dtoEntidad)
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

        public async Task<BaseDTO> Obtener(long id)
        {
            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.Obtener(id, "Persona");

            if (entidad == null) throw new Exception("No se encotró la Usuario que esta buscando");

            return new UsuarioDTO
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Password = entidad.Password,
                Rol = entidad.Rol,
                PersonaId = entidad.PersonaId,
                Persona = new PersonaDTO
                {
                    Id = entidad.Persona.Id,
                    Nombre = entidad.Persona.Nombre,
                    Apellido = entidad.Persona.Apellido,
                    Dni = entidad.Persona.Dni,
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
                PersonaId = x.PersonaId,
                Persona = new PersonaDTO {
                    Id = x.Persona.Id,
                    Nombre = x.Persona.Nombre,
                    Apellido = x.Persona.Apellido,
                    Dni = x.Persona.Dni,
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
                PersonaId=x.PersonaId,
                Persona = new PersonaDTO
                {
                    Id = x.Persona.Id,
                    Nombre = x.Persona.Nombre,
                    Apellido = x.Persona.Apellido,
                    Dni = x.Persona.Dni,
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
