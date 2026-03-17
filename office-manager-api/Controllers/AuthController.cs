using Microsoft.AspNetCore.Mvc;

namespace office_manager_api.Controllers
{
    [ApiController] // Indique que cette classe est un contrôleur API
    [Route("api/[controller]")] // Définit l'URL : api/auth
    public class AuthController : ControllerBase
    {
        // Exemple d'un point de terminaison (Endpoint) pour tester
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { message = "API is running" });
        }
    }
}
