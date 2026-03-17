using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using office_manager_api.Models;

namespace office_manager_api.Data.Configs
{
    internal class ResourceConfig:IEntityTypeConfiguration<Resource> 
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources");// Nom de la table dans la base de données
            builder.HasKey(r => r.Id)    // Définition de la clé primaire
                .HasName("PK_Resources"); 
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(r => r.Type) // "Salle" або "Matériel"
 
               .IsRequired()
                .HasMaxLength(50);

            // Ajout de la capacité et de l'emplacement
            builder.Property(r => r.Capacity)
                .IsRequired();
            builder.Property(r => r.Location)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}