using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Constantes;

namespace Infraestructura.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().HasData(new Empleado
            {
                Id = 1,
                Apellido = "admin",
                Nombre = "Usuario",
                Dni = "99999999",
                Direccion = "Rivadavia 1050",
                Mail = "admin@gmail.com",
                Telefono = "9999999",
                AreaTrabajo = "Cobranzas",
                EstaEliminado = false,
            });

            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Nombre = "Admin",
                Password = EncriptarPassword.GetSHA256("1234"),
                Rol = Aplicacion.Constantes.Enums.Rol.Admin,
                Fecha = DateTime.Now,
                PersonaId = 1,
            });
        }
    }
}
