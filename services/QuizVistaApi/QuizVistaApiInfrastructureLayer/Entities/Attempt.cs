using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

public partial class Attempt
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? EditionDate { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual User User { get; set; } = null!;
}
