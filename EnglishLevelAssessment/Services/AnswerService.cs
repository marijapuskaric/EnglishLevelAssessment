using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class AnswerService
    {
		IDbContextFactory<EnglishLevelAssessmentContext> _context;

		public AnswerService(IDbContextFactory<EnglishLevelAssessmentContext> context)
        {
            _context = context;
        }

        public async Task<List<Answer>> GetAnswersForQuestion(int questionId)
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var list = await dbCtx.Answers.Where(p => p.QuestionId == questionId).AsNoTracking().ToListAsync();
				return list;
			}
				
        }

        public async Task<Answer> GetAnswerById(int id)
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var answer = await dbCtx.Answers
						.Select(p => new Answer
						{
							Id = p.Id,
							Text = p.Text,
							QuestionId = p.QuestionId,
							IsCorrect = p.IsCorrect,
							CreatedAt = p.CreatedAt,
							IsDeleted = p.IsDeleted
						}).Where(p => p.Id == id).FirstOrDefaultAsync();
				return answer;
			}	
        }
    }
}
