using CrudUsers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CrudUsers.Context;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Usuarios> Usuarios => Set<Usuarios>();
    public DbSet<Actividades> Actividades => Set<Actividades>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Actividades)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuarios>().HasData(
            new Usuarios
            {
                Id = 1,
                Nombre = "Roy Roger",
                Apellido = "Martinez Cano",
                Correo = "roymartinez94@gmail.com",
                Telefono = 50584892771,
                PaisResidencia = "Nicaragua"
            }
        );

        modelBuilder.Entity<Actividades>().HasData(
            new Actividades
            {
                Id = 1,
                UsuarioId = 1,
                FechaCreacion = new DateTime(2024, 3, 23, 12, 0, 0),
                Actividad = (int)ListaActividad.Creacion
            }
        );
    }
}
