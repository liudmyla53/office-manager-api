namespace office_manager_api.Models
{
    /// <summary>
    /// Représente un utilisateur dans le système Office Manager.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        /// <summary>
        /// Le mot de passe stocké sous forme de hash pour la sécurité.
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// Rôle de l'utilisateur (ex: "Admin", "Employee").
        /// </summary>
        public string Role { get; set; } = "Employee"; // Default role is Employee
        
        /// <summary>
        /// Constructeur privé requis par Entity Framework Core.
        /// </summary>
        public User() { }

        /// <summary>
        /// Constructeur utilisé pour créer un nouvel utilisateur dans le système.
        /// </summary>
        /// <param name="email">Adresse email de l'utilisateur.</param>
        /// <param name="passwordHashed">Mot de passe déjà haché.</param>
        /// <param name="role">Rôle assigné (par défaut "Employee").</param>
        public User(string email, string? passwordHashed, string role = "Employee")
        {
            Email = email;
            Password = passwordHashed;
            Role = role;
        }

        /// <summary>
        /// Constructeur utilisé lors de la récupération d'un utilisateur existant (sans charger le mot de passe).
        /// </summary>
        public User(int id, string email, string role)
        {
            Id = id;
            Email = email;
            Password = null;
            Role = role;
        }
        public bool IsConfirmed { get; set; } = false; // Indique si l'utilisateur a confirmé son compte via email
    }
}
