using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using office_manager_api.Data;
using office_manager_api.Models;

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
        public async Task<IActionResult> GetAll()
        {
            var resources = await _context.Resources.ToListAsync();
            return Ok(resources); // Retourne la liste des ressources en format JSON
        }

        [Authorize(Roles = "Admin")]
        [HttpPost] // POST api/resources
        public async Task<ActionResult> CreatResource([FromBody] Resource resource)
        {
                       if (resource == null) return BadRequest("Données invalides."); // Validation des données
            _context.Resources.Add(resource); // Ajoute la nouvelle ressource à la base de données
            await _context.SaveChangesAsync(); // Enregistre les changements
            return CreatedAtAction(nameof(GetAll), new { id = resource.Id }, resource); // Retourne la ressource créée avec son ID

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResource(int id) 
        { 
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return NotFound("Resource not found");
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return NoContent(); // Succès, mais pas de contenu à renvoyer
        }
    }
}
