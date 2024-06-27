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
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
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
				
        }

		public async Task<List<Result>> GetResults()
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Include(p => p.StudyProgramme)
											.Include(p => p.AcademicYear)
											.Include(p => p.MaturaGrade)
											.Include(p => p.MaturaLevel)
											.Include(p => p.LanguageLevel)
											.Include(p => p.SelfAssessedLanguageLevel)
											.ToListAsync();
				return list;
			}
			
		}

        public async Task<List<Result>> GetOnlineTestResultsByLanguageLevel(int id)
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.LanguageLevelId == id).ToListAsync();
				return list;
			}
				
        }

		public async Task<List<Result>> GetMaturaResultsByMaturaLevelAndGrade(string maturaLevel, string maturaGrade)
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.MaturaLevel.MaturaLevel1 == maturaLevel && p.MaturaGrade.Grade == maturaGrade).ToListAsync();
				return list;
			}
				
		}

		public async Task<List<Result>> GetSelfAssessmentCorrectByLanguagelevel(int languageLevelId)
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.LanguageLevelId == languageLevelId && p.SelfAssessedLanguageLevelId == p.LanguageLevelId).ToListAsync();
				return list;
			}		
		}
		public async Task<List<Result>> GetSelfAssessmentIncorrectByLanguageLevel(int languageLevelId)
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.LanguageLevelId == languageLevelId && p.SelfAssessedLanguageLevelId != p.LanguageLevelId).ToListAsync();
				return list;
			}	
		}


		public async Task<int> GetSelfAssessmentCorrect()
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.LanguageLevelId == p.SelfAssessedLanguageLevelId).ToListAsync();
				return list.Count();
			}
		}

		public async Task<int> GetSelfAssessmentIncorrectByOne()
		{
			using(var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.LanguageLevelId == (p.SelfAssessedLanguageLevelId - 1) || p.LanguageLevelId == (p.SelfAssessedLanguageLevelId + 1)).ToListAsync();
				return list.Count();
			}
		}
		public async Task<int> GetSelfAssessmentIncorrectByMultiple()
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.LanguageLevelId != (p.SelfAssessedLanguageLevelId - 1) && p.LanguageLevelId != (p.SelfAssessedLanguageLevelId + 1) && p.LanguageLevelId != p.SelfAssessedLanguageLevelId).ToListAsync();
				return list.Count();
			}
		}


		public async Task<List<Result>> GetResultsByLanguageLevelAndStudyProgramme(int languageLevelId, string studyProgramme)
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.Results.Where(p => p.LanguageLevelId == languageLevelId)
												.Join(dbCtx.StudyProgrammes,
													  t1 => t1.StudyProgrammeId,
													  t2 => t2.Id,
													  (t1, t2) => new { Results = t1, StudyProgramme = t2 })
												.Where(join => join.StudyProgramme.Programme.Contains(studyProgramme))
												.Select(join => join.Results)
												.AsNoTracking()
												.ToListAsync();
				return list;
			}
				
		}

		public async Task<double> GetNumberOfMaturaResultsByLanguageLevel(int id)
		{
			double num = 0;
			if (id == 1)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade("B (osnovna)", "Dovoljan (2)")).Count;
                num += (await GetMaturaResultsByMaturaLevelAndGrade("B (osnovna)", "Dobar (3)")).Count;
            }
			else if (id == 2)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade("B (osnovna)", "Vrlo dobar (4)")).Count;
				num += (await GetMaturaResultsByMaturaLevelAndGrade("B (osnovna)", "Odličan (5)")).Count;
			}
			else if (id == 3)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade("B (osnovna)", "Odličan (5) više od 95%")).Count;
                num += (await GetMaturaResultsByMaturaLevelAndGrade("A (viša)", "Dovoljan (2)")).Count;
                num += (await GetMaturaResultsByMaturaLevelAndGrade("A (viša)", "Dobar (3)")).Count;
            }
			else if (id == 4)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade("A (viša)", "Vrlo dobar (4)")).Count;
				num += (await GetMaturaResultsByMaturaLevelAndGrade("A (viša)", "Odličan (5)")).Count;
            }
			else if (id == 5)
			{
				num = (await GetMaturaResultsByMaturaLevelAndGrade("A (viša)", "Odličan (5) više od 95%")).Count;
			}
			else if (id == 6)
			{
				num = 0;
			}
			return num;
		}
	}
}
