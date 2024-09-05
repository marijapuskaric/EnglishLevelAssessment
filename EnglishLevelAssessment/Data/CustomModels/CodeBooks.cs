namespace EnglishLevelAssessment.Data.CustomModels
{
    public enum ChartTypes
    {
        LanguageLevel,
        Comparison,
        SelfAssessmentCorrectness,
    }

    public enum ChartDataTypes
    {
        OnlineTest,
        Matura,
        SelfAssessment,
        Racunarstvo,
        Elektrotehnika,
        Prijediplomski,
        Diplomski,
        MaturaOnlineTest,
        SelfAssessmentCorrectness,
        Godina_1,
        Godina_2,
        Godina_3,
        Godina_4,
        Godina_5,
        SelfAssessmentOnlineTest
    }

    public enum MaturaLevels
    {
        A = 1,
        B = 2
    }

    public enum MaturaGrades
    {
        Odlican_95 = 5,
        Odlican = 6,
        Vrlo_dobar = 7,
        Dobar = 8,
        Dovoljan = 9
    }

    public enum LanguageLevels
    {
        A1 = 1,
        A2 = 2,
        B1 = 3,
        B2 = 4,
        C1 = 5,
        C2 = 6
    }

    public enum SelfAssessmentCorrect
    {
        Correct,
        Off_by_1,
        Off_by_multiple
    }

    public enum Comparison
    {
        Less,
        Equal,
        More
    }

    public enum AcademicYears
    {
        godina_1 = 1,
        godina_2 = 2,
        godina_3 = 3
    }
}
