using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class ResultService
    {
        private EnglishLevelAssessmentContext _context;

        public ResultService(EnglishLevelAssessmentContext context)
        {
            _context = context;
        }

        public async Task AddResult(Result result)
        {
            Result entry = await _context.Results.FindAsync(result.Id) ?? new();
            entry.Id = result.Id;
            entry.StudyProgrammeId = result.StudyProgrammeId;
            entry.AcademicYearId = result.AcademicYearId;
            entry.MaturaLevelId = result.MaturaLevelId;
            entry.MaturaGradeId = result.MaturaGradeId;
            entry.NumberOfQuestions = result.NumberOfQuestions;
            entry.NumberOfCorrectAnswers = result.NumberOfCorrectAnswers;
            entry.LanguageLevelId = result.LanguageLevelId;
            entry.CreatedAt = DateTime.Now;
            entry.IsDeleted = result.IsDeleted;
            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Result>> GetResults()
        {
            var list = await _context.Results.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<List<double>> GetNumberOfResultsPerLanguageLevel()
        {
            var count = new List<double>();
            var list = await _context.Results.GroupBy(p => p.LanguageLevelId).ToListAsync();
            foreach (var result in list) 
            {
                count.Add(result.Count());
            }
            return count;
        }
    }
}
