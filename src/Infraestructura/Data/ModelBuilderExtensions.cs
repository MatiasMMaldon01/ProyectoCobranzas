using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Constantes;
using Aplicacion.Constantes.Enums;
using System.Drawing;

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
            },
            new Ciudad
            {
                Id = 2,
                Descripcion = "Concepción",
                EstaEliminado = false,
            });

            modelBuilder.Entity<Extension>().HasData(new Extension
            {
                Id = 1,
                Descripcion = "Casa Central",
                EstaEliminado = false,
            },
            new Extension
            {
                Id= 2,
                Descripcion = "Anexo",
                EstaEliminado = false
            });

            modelBuilder.Entity<Contador>().HasData(new  Contador
            { 
                Id = 1,
                Entidad = Entidad.Persona,
                Valor = 1, 
                EstaEliminado = false
            },
            new Contador
            { 
                Id = 2,
                Entidad = Entidad.Pago,
                Valor = 0, 
                EstaEliminado = false
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
                CodigoPostal = 4000,
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
