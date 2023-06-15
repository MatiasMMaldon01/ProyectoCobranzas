using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Constantes;
using Aplicacion.Constantes.Enums;

namespace Infraestructura.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Ciudad>().HasData(new Ciudad
            {
                Id = 1,
                Descripcion = "Capital",
                EstaEliminado = false,
            });

            modelBuilder.Entity<Extension>().HasData(new Extension
            {
                Id = 1,
                Descripcion = "Casa Central",
                EstaEliminado = false,
            });

            modelBuilder.Entity<Empleado>().HasData(new Empleado
            {
                Id = 1,
                Apynom = "Admin",
                TipoDoc = TipoDocumento.DNI,
                NroDoc = "99999999",
                Direccion = "Rivadavia 1050",
                Mail = "admin@gmail.com",
                Telefono = "9999999",
                AreaTrabajo = "Cobranzas",
                CiudadId = 1,
                ExtensionId = 1,
                EstaEliminado = false,
            });

            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Nombre = "Admin",
                Password = EncriptarPassword.GetSHA256("1234"),
                Rol = Rol.Admin,
                Fecha = DateTime.Now,
                PersonaId = 1,
            });
        }
    }
}
