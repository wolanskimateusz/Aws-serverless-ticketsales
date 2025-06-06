using api.Dtos.Artist;
using api.Models;

namespace api.Interfaces
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(int id);
        Task<Artist?> CreateAsync(Artist artistModel);
        Task<Artist?> UpdateAsync(int id, UpdateArtistDto artistDto);
        Task<Artist?> DeleteAsync(int id);
        Task<List<GenreDto>> GetGenresAsync();
        Task<List<Artist>?> GetArtistsByGenreAsync(string genre);
        Task<Artist?> GetArtistByNameAsync(string name);
           

    }
}
