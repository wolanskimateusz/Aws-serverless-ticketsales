using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AppUser> GetByNameAsync(string name)
        {
            var result = await _context.Users
            .Include(u => u.Tickets) // Załaduj listę biletów
            .ThenInclude(t => t.Event) // Załaduj powiązane eventy
            .FirstOrDefaultAsync(u => u.UserName == name);
            if (result == null) return null;
            return result;
        }
    }
}
