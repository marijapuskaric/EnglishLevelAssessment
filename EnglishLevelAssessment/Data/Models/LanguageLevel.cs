using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class LanguageLevel
{
    public int Id { get; set; }

    public string? Level { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
