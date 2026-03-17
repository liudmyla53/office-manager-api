using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using office_manager_api.Data;
using office_manager_api.Models;
using System.Security.Claims;

namespace office_manager_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Nécessite une authentification pour accéder à ces endpoints
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }
         
       // 2. Créer une nouvelle réservation (POST api/reservations)
        [HttpPost] 
        public IActionResult Create([FromBody] Reservation reservation)
        {
            var userIdClaim= User.FindFirst(ClaimTypes.NameIdentifier);
            // Validation basique (à améliorer selon les besoins)
            if (userIdClaim == null) return Unauthorized();
            // L'ID utilisateur est lié automatiquement depuis le token
            reservation.UserId = int.Parse(userIdClaim.Value);

            // Ajouter la réservation à la base de données
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return Ok(new { message = "Réservation créée avec succès !", reservation });
        }
        // Supprimer une réservation spécifique
        [HttpDelete("{id}")] // DELETE api/reservations/{id}
        public IActionResult Delete(int id)
        {
            // On récupère l'ID de l'utilisateur qui fait la requête
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            // On cherche la réservation dans la base de données
            var reservation = _context.Reservations.Find(id);

                if (reservation == null) 
                    return NotFound("Réservation non trouvée");
            // Vérification de propriété : seul l'auteur peut supprimer sa réservation
            if (reservation.UserId!= userId) 
                return Forbid("Vous n'avez pas l'autorisation de supprime cette réservation");

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return NoContent(); // Succès, mais pas de contenu à renvoyer

        }

        // 1. Récupérer toutes les réservations (GET api/reservations)
        [HttpGet] 
        public IActionResult GetAll()
        {
            // On inclut les détails de l'utilisateur et de la ressource pour plus d'infos
            var reservations = _context.Reservations.ToList();
            return Ok(reservations);
        }
    }
}
