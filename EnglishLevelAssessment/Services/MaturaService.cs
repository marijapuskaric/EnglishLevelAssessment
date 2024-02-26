using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class MaturaService
    {
        private EnglishLevelAssessmentContext _context;

        public MaturaService(EnglishLevelAssessmentContext context)
        {
            _context = context;
        }

        public async Task<List<MaturaLevel>> GetMaturaLevels()
        {
            var list = await _context.MaturaLevels.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<List<MaturaGrade>> GetMaturaGrades()
        {
            var list = await _context.MaturaGrades.AsNoTracking().ToListAsync();
            return list;
        }
    }
}
