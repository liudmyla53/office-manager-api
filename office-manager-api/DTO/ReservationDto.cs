namespace office_manager_api.DTO
{
    public class ReservationDto
    {
       public int ResourceId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Purpose { get; set; } = string.Empty;
        public bool IsRecurring { get; set; }
        public string? RecurrencePattern { get; set; }
    }
}
