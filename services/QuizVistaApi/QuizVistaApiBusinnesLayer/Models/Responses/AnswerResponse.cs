using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QuizVistaApiBusinnesLayer.Models.Responses;



public class AnswerResponse
{
    public int Id { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    public int AttemptId { get; set; }

    public AttemptResponse? Attempt { get; set; }

    public QuestionResponse? Question { get; set; }

    private AnswerResponse() { }

    public AnswerResponse(
        int id,
        string answerText,
        bool isCorrect,
        int questionId,
        int attemptId,
        AttemptResponse attempt,
        QuestionResponse question
        )
    {
        Id = id;
        AnswerText = answerText;
        IsCorrect = isCorrect;
        QuestionId = questionId;
        AttemptId = attemptId;
        Attempt = attempt;
        Question = question;
    }
    public static AnswerResponse Convert(this Answer answer)
    {
        return new AnswerResponse
        (
            answer.Id,
            answer.AnswerText,
            answer.IsCorrect,
            answer.QuestionId,
            answer.AttemptId,
            answer.Attempt,
            answer.Question
        );
    }
}
