namespace Quiz_project.Dtos
{
    public class RegisterDto
    {
        public required string Name { get; set; } 
        public required string Email { get; set; } 
        public required string Password { get; set; } 
        public string? Role { get; set; } // Optional role field, defaults to "User" if not provided
    }
}
