namespace office_manager_api.DTO
{
    public class RegisterDto
    {
        public string firstName { get; set; } = default!;
        public string lastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
