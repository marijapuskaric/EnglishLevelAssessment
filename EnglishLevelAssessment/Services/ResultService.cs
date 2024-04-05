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

        public async Task<List<Result>> GetOnlineTestResultsByLanguageLevel(int id)
        {
            var list = await _context.Results.Where(p => p.LanguageLevelId == id).ToListAsync();
            return list;
        }

		public async Task<List<Result>> GetMaturaResultsByMaturaLevelAndGrade(int maturaLevelId, int maturaGradeId)
		{
			var list = await _context.Results.Where(p => p.MaturaLevelId == maturaLevelId && p.MaturaGradeId == maturaGradeId).ToListAsync();
			return list;
		}

		

		public async Task<List<Result>> GetResultsByLanguageLevelAndStudyProgramme(int languageLevelId, string studyProgramme)
		{
			var list = await _context.Results.Where(p => p.LanguageLevelId == languageLevelId)
			.Join(_context.StudyProgrammes,
				  t1 => t1.StudyProgrammeId,
				  t2 => t2.Id,
				  (t1, t2) => new { Results = t1, StudyProgramme = t2 })
			.Where(join => join.StudyProgramme.Programme.Contains(studyProgramme))
			.Select(join => join.Results)
			.AsNoTracking()
			.ToListAsync();
			return list;
		}

		public async Task<double> GetNumberOfMaturaResultsByLanguageLevel(int id)
		{
			double num = 0;
			if (id == 1)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade(2, 4)).Count;
			}
			else if (id == 2)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade(2, 3)).Count;
				num += (await GetMaturaResultsByMaturaLevelAndGrade(2, 2)).Count;
			}
			else if (id == 3)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade(2, 1)).Count;
			}
			else if (id == 4)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade(1, 4)).Count;
				num += (await GetMaturaResultsByMaturaLevelAndGrade(1, 3)).Count;
			}
			else if (id == 5)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade(1, 2)).Count;
			}
			else if (id == 6)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade(1, 1)).Count;
			}
			return num;
		}
	}
}
