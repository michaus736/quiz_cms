using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

[Entity]
public partial class Attempt
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? EditionDate { get; set; }

    public int UsersId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Answer> AnswersNavigation { get; set; } = new List<Answer>();
}
