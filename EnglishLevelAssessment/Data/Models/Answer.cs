using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class Answer
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int? QuestionId { get; set; }

    public bool? IsCorrect { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Question? Question { get; set; }
}
