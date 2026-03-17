

using Microsoft.EntityFrameworkCore;
using office_manager_api.Models;

namespace office_manager_api.Data
{
    /// <summary>
    /// Le contexte de base de données principal pour l'application.
    /// Il fait le lien entre les classes C# et les tables SQL Server.
    /// </summary>
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Définition des tables
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Resource> Resources { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuration supplémentaire (Fluent API)

            // Un utilisateur peut avoir plusieurs réservation
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
    }
}
