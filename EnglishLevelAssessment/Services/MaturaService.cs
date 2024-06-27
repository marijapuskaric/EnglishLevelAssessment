using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class MaturaService
    {
		IDbContextFactory<EnglishLevelAssessmentContext> _context;

		public MaturaService(IDbContextFactory<EnglishLevelAssessmentContext> context)
        {
            _context = context;
        }

        public async Task<List<MaturaLevel>> GetMaturaLevels()
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var list = await dbCtx.MaturaLevels.AsNoTracking().ToListAsync();
				return list;
			}
        }

        public async Task<List<MaturaGrade>> GetMaturaGrades()
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var list = await dbCtx.MaturaGrades.AsNoTracking().ToListAsync();
				return list;
			}
        }
    }
}
