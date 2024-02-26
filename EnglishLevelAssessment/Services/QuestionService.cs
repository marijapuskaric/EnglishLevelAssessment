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

        public async Task<List<Question>> GetNumberOfQuestionsByLevel(string level, int num)
        {
            Random rand = new Random();
            //int skipper = rand.Next(0, _context.Questions.Where(p => p.LanguageLevel == level).Count());
            //var list = await _context.Questions
            //                        .Where(p => p.LanguageLevel == level)
            //                        .OrderBy(product => Guid.NewGuid())
            //                        .Skip(skipper)
            //                        .Take(5)
            //                        .AsNoTracking()
            //                        .ToListAsync();
           // var list = await _context.Questions.Where(p => p.LanguageLevel == level).ToListAsync();
           var list = await _context.Questions.Where(p => p.LanguageLevel == level).OrderBy(p => Guid.NewGuid()).Take(num).ToListAsync();
            
            return list;
        }

        public async Task<List<Question>> GetQuestions()
        {
            var A1Questions = await GetNumberOfQuestionsByLevel("A1", 5);
            var A2Questions = await GetNumberOfQuestionsByLevel("A2", 5);
            var B1Questions = await GetNumberOfQuestionsByLevel("B1", 5);
            var B2Questions = await GetNumberOfQuestionsByLevel("B2", 5);
            var C1Questions = await GetNumberOfQuestionsByLevel("C1", 5);
            var C2Questions = await GetNumberOfQuestionsByLevel("C2", 5);

            var questions =A1Questions.Concat(A2Questions).Concat(B1Questions).Concat(B2Questions).Concat(C1Questions).Concat(C2Questions).ToList();

            return questions.OrderBy(p => Guid.NewGuid()).ToList();

        }
    }
}
