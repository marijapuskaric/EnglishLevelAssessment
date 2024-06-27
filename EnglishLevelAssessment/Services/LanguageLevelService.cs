using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class LanguageLevelService
    {
		IDbContextFactory<EnglishLevelAssessmentContext> _context;

		public LanguageLevelService(IDbContextFactory<EnglishLevelAssessmentContext> context) 
        { 
            _context = context;
        }

        public async Task<LanguageLevel> GetLanguageLevelById(int id)
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var level = await dbCtx.LanguageLevels
						.Select(p => new LanguageLevel
						{
							Id = p.Id,
							Level = p.Level,
							Description = p.Description
						}).Where(p => p.Id == id).FirstOrDefaultAsync();
				return level;
			}
				
        }

		public async Task<List<LanguageLevel>> GetLanguageLevels()
		{
			using (var dbCtx = await _context.CreateDbContextAsync())
			{
				var list = await dbCtx.LanguageLevels.AsNoTracking().ToListAsync();
				return list;
			}
		}
	}
}
