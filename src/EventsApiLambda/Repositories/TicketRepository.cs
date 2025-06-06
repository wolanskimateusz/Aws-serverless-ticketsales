using api.Data;
using api.Dtos.Ticket;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;
        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> CreateAsync(Ticket ticketModel)
        {
           
           await _context.AddAsync(ticketModel);
           await _context.SaveChangesAsync();
           return ticketModel;
        }

        public async Task<Ticket?> DeleteAsync(int id)
        {
            var result = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)  return null; 
            _context.Tickets.Remove(result);
            await _context.SaveChangesAsync();
            return result;

        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            var results = await _context.Tickets.Include(x => x.Event).ToListAsync();
            return results;
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Ticket?> UpdateAsync(int id, UpdateTicketDto ticketDto)
        {
            var result = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return null;

            result.Price = ticketDto.Price;
            result.Type = ticketDto.Type;
            result.Event.Name = ticketDto.EventName;

            await _context.SaveChangesAsync();

            return result;

        }
    }
}
