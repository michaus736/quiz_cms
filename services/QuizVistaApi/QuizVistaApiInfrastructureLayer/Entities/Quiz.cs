using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

public partial class Quiz
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? EditionDate { get; set; }

    public string? Author { get; set; }

    public int CategoryId { get; set; }

    public string? CmsTitleStyle { get; set; }

    public int UserId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
