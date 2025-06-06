using api.Dtos.Artist;
using api.Models;

namespace api.Mappers
{
    public static class ArtistMapper
    {
        public static ArtistDto ToArtistDto(this Artist artistModel)
        {
            return new ArtistDto
            {
                Id = artistModel.Id,
                Name = artistModel.Name,
                Genre = artistModel.Genre
            };
        }

        public static Artist ToArtistFromCreateDto(this CreateArtistDto createDto)
        {
            return new Artist 
            { 
                Name = createDto.Name,
                Genre = createDto.Genre
            };
        }

        public static GenreDto ToGenreDto(this GenreDto genreDto)
        {
            return new GenreDto
            {
                Name = genreDto.Name
            };
        }
    }
}
