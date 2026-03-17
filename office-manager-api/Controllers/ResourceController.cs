using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using office_manager_api.Data;

namespace office_manager_api.Controllers
{
    [ApiController] // Indique que cette classe est un contrôleur API
    [Route("api/[controller]")] // Définit l'URL : api/resources
    [Authorize] // Nécessite une authentification pour accéder à ces endpoints
    public class ResourceController : ControllerBase
    {
        private readonly AppDbContext _context;
        // Injection du contexte de base de données via le constructeur
        public ResourceController(AppDbContext context)
        {
            _context = context;
        }
        // Récupère la liste complète des ressources (salles, matériel)
        [HttpGet] // GET api/resources
        public IActionResult GetAll()
        {
            var resources = _context.Resources.ToListAsync();
            return Ok(resources); // Retourne la liste des ressources en format JSON
        }
    }
}
