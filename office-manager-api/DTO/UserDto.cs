namespace office_manager_api.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;


        public string Token { get; set; } = default!;
    }
}
