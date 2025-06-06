using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetByNameAsync(string name);
    }
}
