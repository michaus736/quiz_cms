using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses;

public class UserResponse
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public virtual List<AttemptResponse> Attempts { get; set; } = new List<AttemptResponse>();

    public virtual List<QuizResponse> QuizzesNavigation { get; set; } = new List<QuizResponse>();

    public virtual List<QuizResponse> Quizzes { get; set; } = new List<QuizResponse>();

    public virtual List<RoleResponse> Roles { get; set; } = new List<RoleResponse>();

    private UserResponse() { }

    public UserResponse(int id, 
        string userName,
        string passwordHash,
        string firstName, 
        string lastName, 
        string email,
        List<AttemptResponse> attempts, 
        List<QuizResponse> quizzesNavigation, 
        List<QuizResponse> quizzes,
        List<RoleResponse> roles)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Attempts = attempts;
        QuizzesNavigation = quizzesNavigation;
        Quizzes = quizzes;
        Roles = roles;
    }

    public static UserResponse Convert(this User user)
    {
        return new UserResponse(
            user.Id,
            user.UserName,
            user.PasswordHash,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Attempts,
            user.QuizzesNavigation,
            user.Quizzes,
            user.Roles
        );
    }
}
