using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses;


public class TagResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual List<QuizResponse> Quizzes { get; set; } = new List<QuizResponse>();

    private TagResponse() { }

    public TagResponse(int id, string name, List<QuizResponse> quizzes)
    {
        Id = id;
        Name = name;
        Quizzes = quizzes;
    }

    public static TagResponse Convert(this Tag tag)
    {
        return new TagResponse(
            tag.Id,
            tag.Name,
            tag.Quizzes
        );
    }
}
