using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class CollegeService
    {
        private EnglishLevelAssessmentContext _context;

        public CollegeService(EnglishLevelAssessmentContext context)
        {
            _context = context;
        }

        public async Task<List<AcademicYear>> GetAcademicYears()
        {
            var list = await _context.AcademicYears.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<List<StudyProgramme>> GetStudyProgrammes()
        {
            var list = await _context.StudyProgrammes.AsNoTracking().ToListAsync();
            return list;
        }
    }
}
