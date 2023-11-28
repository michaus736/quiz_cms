using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses;


public class QuizResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? EditionDate { get; set; }

    public int CategoryId { get; set; }

    public string? CmsTitleStyle { get; set; }

    public int UserId { get; set; }

    public CategoryResponse? Category { get; set; }

    public List<QuestionResponse> Questions { get; set; } = new List<QuestionResponse>();

    public UserResponse? User { get; set; }

    public List<TagResponse> Tags { get; set; } = new List<TagResponse>();

    public List<UserResponse> Users { get; set; } = new List<UserResponse>();

    private QuizResponse() { }

    public QuizResponse(int id, string name, string? description, DateTime creationDate, DateTime? editionDate, int categoryId, string? cmsTitleStyle, int userId, CategoryResponse? category, List<QuestionResponse> questions, UserResponse? user, List<TagResponse> tags, List<UserResponse> users)
    {
        Id = id;
        Name = name;
        Description = description;
        CreationDate = creationDate;
        EditionDate = editionDate;
        CategoryId = categoryId;
        CmsTitleStyle = cmsTitleStyle;
        UserId = userId;
        Category = category;
        Questions = questions;
        User = user;
        Tags = tags;
        Users = users;
    }

    public static QuizResponse Convert(this Quiz quiz)
    {
        return new QuizResponse(
            quiz.Id,
            quiz.Name,
            quiz.Description,
            quiz.CreationDate,
            quiz.EditionDate,
            quiz.CategoryId,
            quiz.CmsTitleStyle,
            quiz.UserId,
            quiz.Category,
            quiz.Questions,
            quiz.User,
            quiz.Tags
        );
    }
}
