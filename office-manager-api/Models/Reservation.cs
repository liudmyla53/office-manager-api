namespace office_manager_api.Models
{
    /// <summary>
    /// Représente une réservation de salle ou de matériel par un utilisateur.
    /// </summary>
    public class Reservation
    {
        public int Id { get; set; }

        // --- Clés étrangères (Relations) --- 
        public int UserId { get; set; } // Référence à l'utilisateur qui a fait la réservation
        public virtual User User { get; set; } = default!; // Navigation vers l'utilisateur
        public int ResourceId { get; set; } // Référence à la ressource réservée
        public virtual Resource Resource { get; set; } = default!; // Navigation vers la ressource

        // --- Dates et Heures ---
        public DateTime StartTime { get; set; } // Date et heure de début de la réservation
        public DateTime EndTime { get; set; } // Date et heure de fin de la réservation

        public string Purpose { get; set; } = string.Empty; // Optionnel : raison de la réservation (ex: "Réunion d'équipe", "Présentation client")
        public string Status { get; set; } = "Confirmed"; // Statut de la réservation (ex: "Confirmed", "Cancelled", "Pending")


        // --- Logique de Récursion
        /// <summary>
        /// Indique si la réservation est répétitive (ex: tous les lundis).
        /// </summary>
        public bool IsRecurring { get; set; } = false;

        /// <summary>
        /// Le modèle de répétition (ex: "Daily", "Weekly", "Monthly").
        /// </summary>
        public string? RecurrencePattern { get; set; }

        /// <summary>
        /// Date à laquelle la répétition s'arrête.
        /// </summary>
        public DateTime? RecurrenceEndDate { get; set; }
        /// <summary>
        /// Constructeur privé requis par Entity Framework Core.
        /// </summary>
        protected Reservation() { }
        /// <summary>
        /// Constructeur utilisé pour créer une nouvelle réservation.
        /// </summary>
        public Reservation(int userId, int resourceId, DateTime startTime, DateTime endTime, string purpose, bool isRecurring=false, string? pattern = null, DateTime? endDate = null)
        {
            UserId = userId;
            ResourceId = resourceId;
            StartTime = startTime;
            EndTime = endTime;
            Purpose = purpose;
            IsRecurring = isRecurring;
            RecurrencePattern = pattern;
            RecurrenceEndDate = endDate;
            Status = "Confirmed"; // Par défaut, une nouvelle réservation est confirmée
        }
       
    }
}
