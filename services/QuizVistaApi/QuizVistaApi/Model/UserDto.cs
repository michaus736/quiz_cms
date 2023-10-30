namespace QuizVistaApi.Model
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
