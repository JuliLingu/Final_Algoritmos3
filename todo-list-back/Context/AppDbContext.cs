using Microsoft.EntityFrameworkCore;
using todo_list_back.Models;

namespace todo_list_back.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<PermisosRoles> PermisosRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Usuario y Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Roles)  // Un usuario tiene un rol
                .WithMany(r => r.Usuarios)  // Un rol puede tener muchos usuarios
                .HasForeignKey(u => u.Id_Roles_FK)  // Clave foránea en Usuario
                .OnDelete(DeleteBehavior.SetNull);  // Al eliminar un rol, se establece NULL

            base.OnModelCreating(modelBuilder);
        }

    }
}
