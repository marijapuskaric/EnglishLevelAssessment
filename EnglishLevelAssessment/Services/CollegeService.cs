using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class CollegeService
    {
		IDbContextFactory<EnglishLevelAssessmentContext> _context;

		public CollegeService(IDbContextFactory<EnglishLevelAssessmentContext> context)
        {
            _context = context;
        }

        public async Task<List<AcademicYear>> GetAcademicYears()
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var list = await dbCtx.AcademicYears.AsNoTracking().ToListAsync();
				return list;
			}
				
        }

        public async Task<List<AcademicYear>> GetUndergraduateAcademicYears()
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var list = await dbCtx.AcademicYears.Where(p => p.Undergraduate == true).AsNoTracking().ToListAsync();
				return list;
			}
        }

        public async Task<List<AcademicYear>> GetGraduateAcademicYears()
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var list = await dbCtx.AcademicYears.Where(p => p.Graduate == true).AsNoTracking().ToListAsync();
				return list;
			}
        }


        public async Task<List<StudyProgramme>> GetStudyProgrammes()
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var list = await dbCtx.StudyProgrammes.AsNoTracking().ToListAsync();
				return list;
			}
        }

        public async Task<StudyProgramme> GetStudyProgrammeById(int id)
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var studyProgramme = await dbCtx.StudyProgrammes
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
}
