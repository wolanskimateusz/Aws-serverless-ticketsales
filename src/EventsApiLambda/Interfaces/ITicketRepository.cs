using api.Dtos.Event;
using api.Dtos.Ticket;
using api.Models;

namespace api.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket> CreateAsync(Ticket ticketModel);
        Task<Ticket?> UpdateAsync(int id, UpdateTicketDto ticketDto);
        Task<Ticket?> DeleteAsync(int id);
    }
}
