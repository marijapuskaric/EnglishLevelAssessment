using EnglishLevelAssessment.Data.CustomModels;
using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EnglishLevelAssessment.Services
{
    public class ResultService
    {
		IDbContextFactory<EnglishLevelAssessmentContext> _context;

        public ResultService(IDbContextFactory<EnglishLevelAssessmentContext> context)
        {
            _context = context;
        }

        public async Task AddResult(Result result)
        {
            using var dbCtx = await _context.CreateDbContextAsync();
			Result entry = await dbCtx.Results.FindAsync(result.Id) ?? new();
			entry.Id = result.Id;
			entry.StudyProgrammeId = result.StudyProgrammeId;
			entry.AcademicYearId = result.AcademicYearId;
			entry.MaturaLevelId = result.MaturaLevelId;
			entry.MaturaGradeId = result.MaturaGradeId;
			entry.NumberOfQuestions = result.NumberOfQuestions;
			entry.NumberOfCorrectAnswers = result.NumberOfCorrectAnswers;
			entry.LanguageLevelId = result.LanguageLevelId;
			entry.SelfAssessedLanguageLevelId = result.SelfAssessedLanguageLevelId;
			entry.CreatedAt = DateTime.Now;
			entry.IsDeleted = result.IsDeleted;
			await dbCtx.AddAsync(entry);
			await dbCtx.SaveChangesAsync();
        }

        public async Task<List<Vresult>> GetVResults()
        {
            using var dbCtx = await _context.CreateDbContextAsync();
            var list = await dbCtx.Vresults.Where(p => p.IsDeleted != true).ToListAsync();
            return list;
        }

        public async Task<int> GetNumberOfResults()
        {
            using var dbCtx = await _context.CreateDbContextAsync();
            return await dbCtx.Results.CountAsync();
        }

        public async Task<int> GetNumberOfResultsByChartDataType(ChartDataTypes pieChartDataType)
        {
            using var dbCtx = await _context.CreateDbContextAsync();
            int numberOfResults;
            if (pieChartDataType == ChartDataTypes.Racunarstvo)
            {
                numberOfResults = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Računarstvo")).CountAsync();
            }
            else if (pieChartDataType == ChartDataTypes.Elektrotehnika)
            {
                numberOfResults = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Elektrotehnika")).CountAsync();
            }
            else if (pieChartDataType == ChartDataTypes.Prijediplomski)
            {
                numberOfResults = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Prijediplomski")).CountAsync();
            }
            else if (pieChartDataType == ChartDataTypes.Diplomski)
            {
                numberOfResults = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Sveučilišni diplomski")).CountAsync();
            }
            else
            {
                numberOfResults = await dbCtx.Results.CountAsync();
            }
            return numberOfResults;
        }

        public async Task<List<Result>> GetResultsForLanguageLevel(int? languageLevelId, ChartDataTypes pieChartDataType)
        {
            using var dbCtx = await _context.CreateDbContextAsync();
            var list = new List<Result>();
            if (languageLevelId != 0)
            {
                if (pieChartDataType == ChartDataTypes.OnlineTest)
                {
                    list = await dbCtx.Results.Where(p => p.LanguageLevelId == languageLevelId).ToListAsync();
                }
                else if (pieChartDataType == ChartDataTypes.Matura)
                {
                    list = await GetMaturaResultsByLanguageLevel(languageLevelId ?? 0);
                }
                else if (pieChartDataType == ChartDataTypes.SelfAssessment)
                {
                    list = await dbCtx.Results.Where(p => p.SelfAssessedLanguageLevelId == languageLevelId).ToListAsync();
                }
                else if (pieChartDataType == ChartDataTypes.Racunarstvo)
                {
                    list = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Računarstvo") && p.LanguageLevelId == languageLevelId).ToListAsync();
                }
                else if (pieChartDataType == ChartDataTypes.Elektrotehnika)
                {
                    list = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Elektrotehnika") && p.LanguageLevelId == languageLevelId).ToListAsync();
                }
                else if (pieChartDataType == ChartDataTypes.Prijediplomski)
                {
                    list = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Prijediplomski") && p.LanguageLevelId == languageLevelId).ToListAsync();
                }
                else if (pieChartDataType == ChartDataTypes.Diplomski)
                {
                    list = await dbCtx.Results.Where(p => p.StudyProgramme!.Programme!.Contains("Sveučilišni diplomski") && p.LanguageLevelId == languageLevelId).ToListAsync();
                }
            }
            else
            {
                list = await dbCtx.Results.Where(p => p.LanguageLevelId == null).ToListAsync();
            }
            return list;
        }

        public async Task<List<Result>> GetMaturaResultsByLanguageLevel(int LangugageLevelId)
        {
            List<Result> results = new();
            List<Result> temp;
            if (LangugageLevelId == (int)LanguageLevels.A1)
            {
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.B, MaturaGrades.Dovoljan);
                results.AddRange(temp);
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.B, MaturaGrades.Dobar);
                results.AddRange(temp);
            }
            else if (LangugageLevelId == (int)LanguageLevels.A2)
            {
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.B, MaturaGrades.Vrlo_dobar);
                results.AddRange(temp);
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.B, MaturaGrades.Odlican);
                results.AddRange(temp);
            }
            else if (LangugageLevelId == (int)LanguageLevels.B1)
            {
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.B, MaturaGrades.Odlican_95);
                results.AddRange(temp);
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.A, MaturaGrades.Dovoljan);
                results.AddRange(temp);
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.A, MaturaGrades.Dobar);
                results.AddRange(temp);
            }
            else if (LangugageLevelId == (int)LanguageLevels.B2)
            {
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.A, MaturaGrades.Vrlo_dobar);
                results.AddRange(temp);
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.A, MaturaGrades.Odlican);
                results.AddRange(temp);
            }
            else if (LangugageLevelId == (int)LanguageLevels.C1)
            {
                temp = await GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels.A, MaturaGrades.Odlican_95);
                results.AddRange(temp);
            }
            else
            {
                results = new();
            }
            return results;
        }

        public async Task<List<Result>> GetMaturaResultsByMaturaLevelAndGrade(MaturaLevels maturaLevel, MaturaGrades maturaGrade)
        {
            using var dbCtx = await _context.CreateDbContextAsync();
            var list = await dbCtx.Results.Where(p => p.MaturaLevelId == (int)maturaLevel && p.MaturaGradeId == (int)maturaGrade).ToListAsync();
            return list;
        }

        public async Task<List<Result>> GetResultsForSelfAssessmentCorrectness(SelfAssessmentCorrect selfAssessmentCorrect)
        {
            using var dbCtx = await _context.CreateDbContextAsync();
            List<Result> results = new();
            if (selfAssessmentCorrect == SelfAssessmentCorrect.Correct)
            {
                results = await dbCtx.Results.Where(p => p.SelfAssessedLanguageLevelId == p.LanguageLevelId).ToListAsync();
            }
            else if (selfAssessmentCorrect == SelfAssessmentCorrect.Off_by_1)
            {
                results = await dbCtx.Results.Where(p =>
                                                p.LanguageLevelId == (p.SelfAssessedLanguageLevelId - 1) ||
                                                p.LanguageLevelId == (p.SelfAssessedLanguageLevelId + 1))
                                             .ToListAsync();
            }
            else if (selfAssessmentCorrect == SelfAssessmentCorrect.Off_by_multiple)
            {
                results = await dbCtx.Results.Where(p =>
                                                p.LanguageLevelId != (p.SelfAssessedLanguageLevelId - 1) &&
                                                p.LanguageLevelId != (p.SelfAssessedLanguageLevelId + 1) &&
                                                p.LanguageLevelId != p.SelfAssessedLanguageLevelId)
                                             .ToListAsync();
            }
            return results;
        }

        public async Task<List<Result>> GetResultsForMaturaOnlineTest(MaturaOnlineTest maturaOnlineTest)
        {
            using var dbCtx = await _context.CreateDbContextAsync();
            List<Result> results = new();
            List<Result> list = new();
            results = await dbCtx.Results.ToListAsync();
            foreach (var result in results)
            {
                var languageLevelId = GetLanguageLevelIdFromMatura(result.MaturaLevelId ?? 0, result.MaturaGradeId ?? 0);
                if (maturaOnlineTest == MaturaOnlineTest.Less)
                {
                    if (result.LanguageLevelId < languageLevelId || result.LanguageLevelId == null)
                    {
                        list.Add(result);
                    }
                }
                else if (maturaOnlineTest == MaturaOnlineTest.Equal)
                {
                    if (result.LanguageLevelId == languageLevelId)
                    {
                        list.Add(result);
                    }
                }
                else if (maturaOnlineTest == MaturaOnlineTest.More)
                {
                    if (result.LanguageLevelId > languageLevelId)
                    {
                        list.Add(result);
                    }
                }
            }
            return list;
        }
        public int GetLanguageLevelIdFromMatura(int maturaLevelId, int maturaGradeId)
        {
            if (maturaLevelId == (int)MaturaLevels.B && (maturaGradeId == (int)MaturaGrades.Dovoljan || maturaGradeId == (int)MaturaGrades.Dobar))
            {
                return (int)LanguageLevels.A1;
            }
            else if (maturaLevelId == (int)MaturaLevels.B && (maturaGradeId == (int)MaturaGrades.Vrlo_dobar || maturaGradeId == (int)MaturaGrades.Odlican))
            {
                return (int)LanguageLevels.A2;
            }
            else if ((maturaLevelId == (int)MaturaLevels.A && (maturaGradeId == (int)MaturaGrades.Dovoljan || maturaGradeId == (int)MaturaGrades.Dobar)) ||
                     (maturaLevelId == (int)MaturaLevels.B && maturaGradeId == (int)MaturaGrades.Odlican_95))
            {
                return (int)LanguageLevels.B1;
            }
            else if (maturaLevelId == (int)MaturaLevels.A && (maturaGradeId == (int)MaturaGrades.Vrlo_dobar || maturaGradeId == (int)MaturaGrades.Odlican))
            {
                return (int)LanguageLevels.B2;
            }
            else if (maturaLevelId == (int)MaturaLevels.A && maturaGradeId == (int)MaturaGrades.Odlican_95)
            {
                return (int)LanguageLevels.C1;
            }
            else
            {
                return (int)LanguageLevels.C2;
            }
        }
	}
}
