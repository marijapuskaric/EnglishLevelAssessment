using System;
using System.Collections.Generic;

namespace EnglishLevelAssessment.Data.Models;

public partial class Result
{
    public int Id { get; set; }

    public int? StudyProgrammeId { get; set; }

    public int? AcademicYearId { get; set; }

    public int? MaturaGradeId { get; set; }

    public int? MaturaLevelId { get; set; }

    public int? NumberOfQuestions { get; set; }

    public int? NumberOfCorrectAnswers { get; set; }

    public int? LanguageLevelId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual AcademicYear? AcademicYear { get; set; }

    public virtual LanguageLevel? LanguageLevel { get; set; }

    public virtual MaturaGrade? MaturaGrade { get; set; }

    public virtual MaturaLevel? MaturaLevel { get; set; }

    public virtual StudyProgramme? StudyProgramme { get; set; }
}
