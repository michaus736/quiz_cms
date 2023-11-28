using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

public partial class Answer
{
    public int Id { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    public int AttemptId { get; set; }

    public virtual Attempt Attempt { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
