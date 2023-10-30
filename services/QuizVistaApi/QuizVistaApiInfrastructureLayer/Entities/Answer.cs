using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;
[Entity]
public partial class Answer
{
    public int AnswerId { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestionQuestionId { get; set; }

    public virtual Question QuestionQuestion { get; set; } = null!;
}
