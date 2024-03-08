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

        public async Task<Answer> GetAnswerById(int id)
        {
            var answer = await _context.Answers
                        .Select(p => new Answer
                        {
                            Id = p.Id,
                            Answer1 = p.Answer1,
                            QuestionId = p.QuestionId,
                            IsCorrect = p.IsCorrect,
                            CreatedAt = p.CreatedAt,
                            IsDeleted = p.IsDeleted
                        }).Where(p => p.Id == id).FirstOrDefaultAsync();
            return answer;
        }
    }
}
