using api.Data;
using api.Dtos.Event;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using api.Dtos.Artist;
using api.Helpers;


namespace api.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Event> CreateAsync(Event eventModel)
        {
            await _context.AddAsync(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public async Task<Event?> DeleteAsync(int id)
        {
            var result = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (result == null) return null;

            _context.Events.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        
        public async Task<List<Event>> GetAllAsync(QueryObject query)
        {
            var results =  _context.Events.Include(x => x.Artists).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.EventName))
            {
                results = results.Where(x => x.Name.Contains(query.EventName));
            }

            if (query.Date.HasValue)
            {
                results = results.Where(x => x.Date.Date == query.Date.Value.Date);
            }
        
            return await results.ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
           return await _context.Events.Include(x => x.Artists)
           .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event?> UpdateAsync(int id, UpdateEventDto eventDto)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null) return null; 

            result.Name = eventDto.Name;
            result.Localization = eventDto.Localization;
            result.Date = eventDto.Date;
            result.Descripton = eventDto.Descripton;
            result.NormalPrice = eventDto.NormalPrice;
            result.VipPrice = eventDto.VipPrice;

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<Event>?> GetLatestAsync()
        {
            var result = await _context.Events
             .Include(x => x.Artists)
             .Where(x => x.Date >= DateTime.UtcNow) 
             .OrderBy(x => x.Date) 
             .Take(3)
             .ToListAsync();

            if (result == null) return null;

            return result;
        }

        // Artist List

        public async Task<List<Artist>?> AddArtistToEventAsync(int eventId, int artistId)
        {
            var artistResult = await _context.Artists.FirstOrDefaultAsync(a => a.Id == artistId);
            if (artistResult == null) return null;

            var eventResult = await _context.Events.Include(e => e.Artists).FirstOrDefaultAsync(e => e.Id == eventId);
            if (eventResult == null) return null;

            if (!eventResult.Artists.Contains(artistResult))
            {
                eventResult.Artists.Add(artistResult);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();


            return eventResult.Artists;

        }

        public async Task<Artist?> DeleteArtistFromEventAsync(int eventId, int artistId)
        {
            var eventResult = await _context.Events.Include(e => e.Artists).FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventResult == null) return null;

            var artistResult = await _context.Artists.FirstOrDefaultAsync(x => x.Id == artistId);
            if (artistResult == null) return null;

            eventResult.Artists.Remove(artistResult);

            await _context.SaveChangesAsync();

            return artistResult;
        }

        public async Task<List<EventListDto>> GetEventsWtihArtistByIdAsync(int artistId)
        {
            var eventResult = await _context.Events.Include(x => x.Artists)
                .Where(e => e.Artists.Any(a => a.Id == artistId))
                .ToListAsync();
            if (eventResult == null) return null;

            return eventResult.Select(e => e.ToEventListDto()).ToList(); 
        }


    }
}
