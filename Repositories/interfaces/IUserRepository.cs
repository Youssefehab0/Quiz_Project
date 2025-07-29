using Quiz_project.Models;

namespace Quiz_project.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailAndPasswordAsync(string email, string password);
        Task AddAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}
