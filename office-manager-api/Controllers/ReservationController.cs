using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using office_manager_api.Data;
using office_manager_api.DTO;
using office_manager_api.Models;

using System.Security.Claims;

namespace office_manager_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Nécessite une authentification pour accéder à ces endpoints
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }
         
       // 2. Créer une nouvelle réservation (POST api/reservations)
        [HttpPost]
        [Authorize(Roles = "User,Employee,Admin")] // Seuls les utilisateurs avec les rôles "User" ou "Admin" peuvent créer des réservations
        public IActionResult Create([FromBody] ReservationDto dto)
        {
            var userIdClaim= User.FindFirst(ClaimTypes.NameIdentifier);
            // Validation basique (à améliorer selon les besoins)
            if (userIdClaim == null) return Unauthorized();
            // L'ID utilisateur est lié automatiquement depuis le token
            int UserId = int.Parse(userIdClaim.Value);

            // 1.
            var resource = _context.Resources.Find(dto.ResourceId);
            if (resource == null) return NotFound("Ressource inexistante");

            // 2. 
            bool conflict = _context.Reservations.Any(r =>
                r.ResourceId == dto.ResourceId &&
                r.StartTime < dto.EndTime &&
                dto.StartTime < r.EndTime
            );

            if (conflict) return Conflict("Cette ressource est déjà réservée pour ce créneau.");

            // 
            var reservation = new Reservation(
                UserId,
                dto.ResourceId,
                dto.StartTime,
                dto.EndTime,
                dto.Purpose,
                dto.IsRecurring,
                dto.RecurrencePattern
            );

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            // 4. 
            return Ok(new
            {
                message = "Réservation réussie !",
                id = reservation.Id,
                startTime = reservation.StartTime
            });
        }
        //Transfert de réservation
        [HttpPut("{id}/transfer")]
        public IActionResult Transfer(int id, [FromBody] TransferDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var reservation = _context.Reservations.Find(id);

            if (reservation == null) return NotFound("Réservation non trouvée");
            if (reservation.UserId != userId) return Forbid("Ce n'est pas votre réservation");

            // Changement de propriétaire
            reservation.UserId = dto.NewUserId;
            _context.SaveChanges();

            return Ok(new { message = "Réservation transférée" });
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
        [Authorize]
        public IActionResult GetAll()
        {
            // On inclut les détails de l'utilisateur et de la ressource pour plus d'infos
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            //if (userIdClaim == null) return Unauthorized();
            //int userId = int.Parse(userIdClaim.Value);

            var reservations = _context.Reservations
                .Include(r => r.Resource)
                .Include(r => r.User)
                .Where(r => r.StartTime >= DateTime.Now)
                .OrderBy(r=>r.StartTime)
                .ToList();
            return Ok(reservations);
        }
    }
}
