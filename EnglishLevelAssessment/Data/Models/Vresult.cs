using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class Vresult
{
    public string? StudyProgramme { get; set; }

    public string? AcademicYear { get; set; }

    public string? MaturaLevel { get; set; }

    public string? MaturaGrade { get; set; }

    public string? SelfAssessedLanguageLevel { get; set; }

    public int? NumberOfCorrectAnswers { get; set; }

    public int? NumberOfQuestions { get; set; }

    public string? LanguageLevelResult { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
