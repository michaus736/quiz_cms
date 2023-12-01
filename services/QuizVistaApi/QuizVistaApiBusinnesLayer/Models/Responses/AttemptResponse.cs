using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses;



public class AttemptResponse
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? EditionDate { get; set; }

    public int UserId { get; set; }

    public List<AnswerResponse>? Answers { get; set; } = new List<AnswerResponse>();

    public UserResponse? User { get; set; }

    public List<AnswerResponse> AnswersNavigation { get; set; } = new List<AnswerResponse>();

    private AttemptResponse() { }
    public AttemptResponse(int id,
        DateTime createDate,
        DateTime? editionDate,
        int userId, 
        List<AnswerResponse>? answers,
        UserResponse? user,
        List<AnswerResponse> answersNawigation)
    {
        Id = id;
        CreateDate = createDate;
        EditionDate = editionDate;
        UserId = userId;
        Answers = answers;
        User = user;
        AnswersNavigation = answersNawigation;
    }

}
