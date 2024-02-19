﻿using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class StudyProgramme
{
    public int Id { get; set; }

    public string? Programme { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
