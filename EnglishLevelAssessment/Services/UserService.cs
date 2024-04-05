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

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users
                        .Select(p => new User
                        {
                            Id = p.Id,
                            Username = p.Username,
                            Password = p.Password,
                            Role = p.Role
                        }).Where(p => p.Username == username).FirstOrDefaultAsync();
            return user;
        }
    }
}
