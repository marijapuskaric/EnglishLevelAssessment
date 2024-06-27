using EnglishLevelAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Services
{
    public class UserService
    {
		IDbContextFactory<EnglishLevelAssessmentContext> _context;

		public UserService(IDbContextFactory<EnglishLevelAssessmentContext> context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsername(string username)
        {
			using (var dbCtx = await _context.CreateDbContextAsync())
            {
				var user = await dbCtx.Users
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
}
