using Dominio.Entidades;
using Dominio.Interfaces;
using EFCore.BulkExtensions;
using Infraestructura.Data;

namespace Infraestructura.Repositorio
{
    public class CargaMasivaRepositorio<T>: ICargaMasivaRepositorio<T> where T : EntidadBase
    {
        protected readonly DataContext _context;

        public CargaMasivaRepositorio(DataContext context)
        {
            _context = context;
        }

        public async Task CargaMasiva(List<T> registros)
        {
            if (registros == null) throw new Exception("No se encontraron registros en la carga masiva");

            await _context.BulkInsertAsync<T>(registros);

        }
    }
}
