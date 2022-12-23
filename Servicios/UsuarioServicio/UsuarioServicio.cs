using Dominio.Entidades;
using Dominio.Interfaces;
using IServicios.Base.Base_DTO;
using IServicios.Usuario;
using Servicios.Base;
using System.Linq.Expressions;
using IServicios.Usuario.UsuarioDTO;

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
                Password = dto.Password,
                Rol = dto.Rol,
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
            entidad.Password = dto.Password;
            entidad.Rol = dto.Rol;

            await _unidadDeTrabajo.UsuarioRepositorio.Modificar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public async Task<BaseDTO> Obtener(long id)
        {
            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.Obtener(id);

            if (entidad == null) throw new Exception("No se encotró la Usuario que esta buscando");

            return new UsuarioDTO
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Password = entidad.Password,
                Rol = entidad.Rol,
                Eliminado = entidad.EstaEliminado
            };


        }

        public async Task<IEnumerable<BaseDTO>> ObtenerTodos()
        {

            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerTodos();

            return entidad.Select(x => new UsuarioDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Password = x.Password,
                Rol = x.Rol,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Nombre)
                .ToList();
        }

        public async Task<IEnumerable<BaseDTO>> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<Usuario, bool>> filtro = x => x.Nombre.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            var entidad = await _unidadDeTrabajo.UsuarioRepositorio.Obtener(filtro);

            return entidad.Select(x => new UsuarioDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Password = x.Password,
                Rol = x.Rol,
                Eliminado = x.EstaEliminado
            })
                .OrderBy(x => x.Nombre)
                .ToList();
        }

    }
}
