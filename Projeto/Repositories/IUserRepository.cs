using Projeto.Features.User;
using Projeto.Features.User.Queries;

namespace Projeto.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(GetAllUsersQuery query);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task SaveChangesAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
