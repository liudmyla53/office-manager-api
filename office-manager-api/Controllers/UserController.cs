using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using office_manager_api.Data;

namespace office_manager_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        // Traduction: Récupère la liste simplifiée de tous les collègues pour le transfert de réservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            // On ne renvoie pas les mots de passe ! Uniquement les infos nécessaires.
            // Traduction: Projection vers un objet anonyme pour la sécurité des données
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new { u.Id, u.FirstName, u.LastName, u.Email })
                .FirstOrDefaultAsync();

            if (user == null) return NotFound("Utilisateur non trouvé");

            return Ok(user);
        }
    }
}
