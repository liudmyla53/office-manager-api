namespace office_manager_api.Models
{
    /// <summary>
    /// Représente une ressource réservable (salle de réunion ou matériel).
    /// </summary>
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!; // "Room" ou "Equipment"
        public int Capacity { get; set; } // Applicable pour les salles de réunion
        public string Location { get; set; } = default!; // Emplacement de la ressource
        /// <summary>
        /// Constructeur privé requis par Entity Framework Core.
        /// </summary>
        protected Resource() { }
        /// <summary>
        /// Constructeur utilisé pour créer une nouvelle ressource.
        /// </summary>
        public Resource(string name, string type, int capacity, string location)
        {
            Name = name;
            Type = type;
            Capacity = capacity;
            Location = location;
        }
      
    }
}
