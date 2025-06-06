using api.Data;
using api.Dtos.Artist;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;
        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Artist?> CreateAsync(Artist artistModel)
        {
            
            await _context.AddAsync(artistModel);
            await _context.SaveChangesAsync();

            return artistModel;
        }

        public async Task<Artist?> DeleteAsync(int id)
        {
            var result = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return null;

            _context.Artists.Remove(result);
            await _context.SaveChangesAsync();
            return result;

        }

        public async Task<List<Artist>> GetAllAsync()
        {
            var result = await _context.Artists.ToListAsync();

            return result;
        }

        public async Task<List<Artist>?> GetArtistsByGenreAsync(string genre)
        {
            var result = await _context.Artists.Where(x => x.Genre == genre).ToListAsync();
            if (!result.Any()) return null;

            return result;
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            var result = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return null;

            return result;
        }

        public async Task<List<GenreDto>> GetGenresAsync()
        {
            var result = await _context.Artists
                .Select(x => new GenreDto { Name = x.Genre })
                .Distinct()
                .ToListAsync();

            return result;
        }

        public async Task<Artist?> UpdateAsync(int id, UpdateArtistDto artistDto)
        {
            var result = await _context.Artists.FirstOrDefaultAsync(x =>x.Id == id);
            if (result == null) return null;

            result.Name = artistDto.Name;
            result.Genre = artistDto.Genre;

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Artist?> GetArtistByNameAsync(string name)
        {
            var result = await _context.Artists.FirstOrDefaultAsync(x => x.Name == name);
            if (result == null) return null;
            return result;
        }
    }
}
