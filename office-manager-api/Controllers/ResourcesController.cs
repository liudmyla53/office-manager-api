using Microsoft.AspNetCore.Mvc;
using office_manager_api.Data;

namespace office_manager_api.Controllers
{
    [ApiController] // Indique que cette classe est un contrôleur API
    [Route("api/[controller]")] // Définit l'URL : api/resources
    public class ResourcesController : ControllerBase
    {
        private readonly AppDbContext _context;
        // Injection du contexte de base de données via le constructeur
        public ResourcesController(AppDbContext context)
        {
            _context = context;
        }
        // Récupère la liste complète des ressources (salles, matériel)
        [HttpGet] // GET api/resources
        public IActionResult GetAll()
        {
            var resources = _context.Resources.ToList();
            return Ok(resources); // Retourne la liste des ressources en format JSON
        }
    }
}
