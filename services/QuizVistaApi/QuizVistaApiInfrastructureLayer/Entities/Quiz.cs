using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

[Entity]
public partial class Quiz
{
    public int QuizId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? EditionDate { get; set; }

    public string? Author { get; set; }

    public int CategoryCategoryId { get; set; }

    public string? CmsTitleStyle { get; set; }

    public virtual Category CategoryCategory { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
