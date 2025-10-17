using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Features.User;
using Projeto.Features.User.Queries;

namespace Projeto.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync(GetAllUsersQuery query)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => !u.Removed)
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Removed)
                return null;
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && !u.Removed);
            return user;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Removed)
                return false;
            user.Removed = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
