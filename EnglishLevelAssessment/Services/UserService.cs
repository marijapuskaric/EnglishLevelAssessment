using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class UserService
    {
        private EnglishLevelAssessmentContext _context;

        public UserService(EnglishLevelAssessmentContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users
                        .Select(p => new User
                        {
                            Id = p.Id,
                            Email = p.Email,
                            Password = p.Password,
                            Role = p.Role
                        }).Where(p => p.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
