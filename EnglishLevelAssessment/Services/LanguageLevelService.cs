using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class LanguageLevelService
    {
        private EnglishLevelAssessmentContext _context;

        public LanguageLevelService(EnglishLevelAssessmentContext context) 
        { 
            _context = context;
        }

        public async Task<LanguageLevel> GetLanguageLevelById(int id)
        {
            var level = await _context.LanguageLevels
                        .Select(p => new LanguageLevel
                        {
                            Id = p.Id,
                            Level = p.Level,
                            Description = p.Description
                        }).Where(p => p.Id == id).FirstOrDefaultAsync();
            return level;
        }

		public async Task<List<LanguageLevel>> GetLanguageLevels()
		{
            var list = await _context.LanguageLevels.AsNoTracking().ToListAsync();		
			return list;
		}
	}
}
