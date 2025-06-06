using api.Dtos.Ticket;
using api.Models;
using System.Runtime.CompilerServices;

namespace api.Mappers
{
    public static class TicketMapper
    {
        public static TicketDto ToTicketDto(this Ticket ticketModel)
        {
           
            return new TicketDto
            {
                Id = ticketModel.Id,
                Type = ticketModel.Type,
                Price = ticketModel.Price,
                EventName = ticketModel.Event.Name,
                UserName = ticketModel.Owner.UserName,
                EventId = ticketModel.EventId
                
            };
        }

        public static Ticket ToTicketFromCreateDto(this CreateTicketDto ticketDto)
        {
            return new Ticket
            {
                Type = ticketDto.Type,
                Price = ticketDto.Price,
                EventId = ticketDto.EventId,
            };
        }
    }
}
