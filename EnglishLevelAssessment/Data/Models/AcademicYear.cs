using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class AcademicYear
{
    public int Id { get; set; }

    public string? Year { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public bool Undergraduate { get; set; }

    public bool Graduate { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
