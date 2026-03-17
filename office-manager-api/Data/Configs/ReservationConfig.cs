using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using office_manager_api.Models;

namespace office_manager_api.Data.Configs
{
    internal class ReservationConfig:IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(r => r.Id)
            .HasName("PK_Reservations"); // Définit la clé primaire et lui donne un nom explicite dans la base de données.
            builder.Property(r => r.StartTime)
                .IsRequired();
            builder.Property(r => r.EndTime)
                .IsRequired();

            builder.Property(r=> r.Purpose)
                .IsRequired()
                .HasMaxLength(500);
           builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Confirmed"); // Valeur par défaut pour le statut de la réservation

            // Configuration de la récursion
            builder.Property(r => r.IsRecurring)
                .HasDefaultValue(false);
            builder.Property(r=> r.RecurrencePattern)
                .HasMaxLength(100);

            // Relations (Clés étrangères)
            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Si l'user est supprimé, ses réservations aussi
            builder.HasOne(r => r.Resource)
                .WithMany()
                .HasForeignKey(r => r.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
