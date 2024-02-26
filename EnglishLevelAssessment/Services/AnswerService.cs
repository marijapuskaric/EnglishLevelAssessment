using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class AnswerService
    {
        private EnglishLevelAssessmentContext _context;

        public AnswerService(EnglishLevelAssessmentContext context)
        {
            _context = context;
        }

        public async Task<List<Answer>> GetAnswersForQuestion(int questionId)
        {
            var list = await _context.Answers.Where(p => p.QuestionId == questionId).AsNoTracking().ToListAsync();
            return list;
        }
    }
}
