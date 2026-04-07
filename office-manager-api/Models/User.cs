namespace office_manager_api.Models
{
    /// <summary>
    /// Représente un utilisateur dans le système Office Manager.
    /// </summary>
    public class User
    {
        public int Id { get; private set; }
        public string Email { get; private set; } = default!;
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        /// <summary>
        /// Le mot de passe stocké sous forme de hash pour la sécurité.
        /// </summary>
        public string Password { get; set; }=default!;
        /// <summary>
        /// Rôle de l'utilisateur (ex: "Admin", "Employee").
        /// </summary>
        public string Role { get; set; } = "Employee"; // Default role is Employee
        
        /// <summary>
        /// Constructeur privé requis par Entity Framework Core.
        /// </summary>
        private User() { }

        /// <summary>
        /// Constructeur utilisé pour créer un nouvel utilisateur dans le système.
        /// </summary>
        /// <param name="email">Adresse email de l'utilisateur.</param>
        /// <param name="passwordHashed">Mot de passe déjà haché.</param>
        /// <param name="role">Rôle assigné (par défaut "Employee").</param>
        public User(string firstName,string lastname, string email, string passwordHashed)
        {
            ValidateName(firstName);
             ValidateName(lastname);
            FirstName = firstName;
            LastName = lastname;
            ValidateEmail(email);
            Email = email;
            Password = passwordHashed;
            Role = "Employee";
        }

        private string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");
            if (!email.Contains("@"))
                throw new ArgumentException("Invalid email format.");
            return email.ToLower();
        }



        private string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            
            return name.Trim(); // Retourne le nom sans espaces inutiles
        }

        /// <summary>
        /// Constructeur utilisé lors de la récupération d'un utilisateur existant (sans charger le mot de passe).
        /// </summary>

        public bool IsConfirmed { get; set; } = false; // Indique si l'utilisateur a confirmé son compte via email

       
    }
}
