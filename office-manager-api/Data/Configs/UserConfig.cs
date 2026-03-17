using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using office_manager_api.Models;

namespace office_manager_api.Data.Configs
{
    internal class UserConfig:IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User>builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id)
                .HasName("PK_Users");

            builder.Property(u=>u.IsConfirmed)
                .HasDefaultValue(false);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(320);

            // Garantit qu'aucun utilisateur n'utilise le même email
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users__Email");

            // Limitation de la taille du hash du mot de passe pour optimiser l'espace
            builder.Property(u => u.Password)
                .HasMaxLength(255);

            // Rôle par défaut défini sur "Employee" pour la sécurité
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Employee");
        }
    }
}
