using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class Question
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int? LanguageLevelId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual LanguageLevel? LanguageLevel { get; set; }
}
