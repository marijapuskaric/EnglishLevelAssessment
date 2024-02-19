using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class MaturaLevel
{
    public int Id { get; set; }

    public string? MaturaLevel1 { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
