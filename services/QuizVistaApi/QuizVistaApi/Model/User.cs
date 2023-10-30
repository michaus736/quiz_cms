﻿namespace QuizVistaApi.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }= string.Empty;
        public string PasswordHash { get; set; }= string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<UserRole> UserRoles { get; set; }
    }
}
