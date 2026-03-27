using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using office_manager_api.Data;
using office_manager_api.Models;
using office_manager_api.Services;

using office_manager_api.DTO;

namespace office_manager_api.Controllers
{
    [ApiController] // Indique que cette classe est un contrôleur API
    [Route("api/[controller]")] // Définit l'URL : api/auth
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthController(AppDbContext context, ITokenService tokenService, IEmailService emailService)
        {
            _context = context;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            // Vérification de l'existence de l'utilisateur
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email.ToLower()))
                return BadRequest("Email already in use");
            // Création de l'utilisateur
            var user = new User
                (
                registerDto.firstName,
                registerDto.lastName,
                registerDto.Email.ToLower(),
                registerDto.Password // Hash du mot de passe 
               // "Employee" // Rôle par défaut
            );

            
            user.IsConfirmed = false; // L'utilisateur doit confirmer son email

            _context.Users.Add(user);
           // await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();

            // Envoi d'un email de confirmation (simplifié)
            
           string confirmationLink = "https://localhost:7000/confirm?email=" + user.Email; // Générer un lien de confirmation réel
            await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your email by clicking this link: {confirmationLink}");
            return Ok(new UserDto { Email = user.Email, Role = user.Role, Token =_tokenService.CreateToken(user) });
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // 1. Recherche d'un utilisateur
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Email == loginDto.Email.ToLower());
            // 2. Si l'utilisateur est introuvable  or Vérification du mot de passe — RETOURNER une erreur (c'est la première méthode)
            if (user == null || user.Password != loginDto.Password)
            {return Unauthorized("Invalid email or password");}

           
            // 3. Si tout se passe bien, RENVOYEZ les données utilisateur (c'est la deuxième méthode)
            var responseDto = new UserDto
            { 
                Id = user.Id,
                Email = user.Email, 
                Role = user.Role, 
                Token = _tokenService.CreateToken(user) 
            };
            return Ok(responseDto);
        }

        // Exemple d'un point de terminaison (Endpoint) pour tester
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { message = "API is running" });
        }
    }
}
