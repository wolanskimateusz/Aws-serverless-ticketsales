using api.Dtos.Event;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IEventRepository
    {

        Task<List<Event>> GetAllAsync(QueryObject query);
        Task<Event?> GetByIdAsync(int id);
        Task<Event> CreateAsync(Event eventModel);
        Task<Event?> UpdateAsync(int id, UpdateEventDto eventDto);
        Task<Event?> DeleteAsync(int id);
        Task<List<Event>?> GetLatestAsync();



        Task<List<Artist>?> AddArtistToEventAsync(int eventId, int artistId);
        Task<Artist?> DeleteArtistFromEventAsync(int eventId, int artistId);
        Task<List<EventListDto>> GetEventsWtihArtistByIdAsync(int artistId);


    }
}
