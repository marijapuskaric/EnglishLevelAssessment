using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static MudBlazor.Colors;

namespace EnglishLevelAssessment.Services
{
    public class QuestionService
    {
        private EnglishLevelAssessmentContext _context;

        public QuestionService(EnglishLevelAssessmentContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetNumberOfQuestionsByLevel(int level, int num)
        {
            Random rand = new Random();
           var list = await _context.Questions.Where(p => p.LanguageLevelId == level).OrderBy(p => Guid.NewGuid()).Take(num).ToListAsync();
            
            return list;
        }

        public async Task<List<Question>> GetQuestions()
        {
            var A1Questions = await GetNumberOfQuestionsByLevel(1, 5);
            var A2Questions = await GetNumberOfQuestionsByLevel(2, 5);
            var B1Questions = await GetNumberOfQuestionsByLevel(3, 5);
            var B2Questions = await GetNumberOfQuestionsByLevel(4, 5);
            var C1Questions = await GetNumberOfQuestionsByLevel(5, 5);
            var C2Questions = await GetNumberOfQuestionsByLevel(6, 5);

            var questions =A1Questions.Concat(A2Questions).Concat(B1Questions).Concat(B2Questions).Concat(C1Questions).Concat(C2Questions).ToList();

            return questions.OrderBy(p => Guid.NewGuid()).ToList();

        }
    }
}
