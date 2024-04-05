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

        public async Task<List<AcademicYear>> GetUndergraduateAcademicYears()
        {
            var list = await _context.AcademicYears.Where(p => p.Undergraduate == true).AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<List<AcademicYear>> GetGraduateAcademicYears()
        {
            var list = await _context.AcademicYears.Where(p => p.Graduate == true).AsNoTracking().ToListAsync();
            return list;
        }


        public async Task<List<StudyProgramme>> GetStudyProgrammes()
        {
            var list = await _context.StudyProgrammes.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<StudyProgramme> GetStudyProgrammeById(int id)
        {
            var studyProgramme = await _context.StudyProgrammes
                        .Select(p => new StudyProgramme
                        {
                            Id = p.Id,
                            Programme = p.Programme,
                            CreatedAt = p.CreatedAt,
                            IsDeleted = p.IsDeleted
                        }).Where(p => p.Id == id).FirstOrDefaultAsync();
            return studyProgramme;
        }
    }
}
