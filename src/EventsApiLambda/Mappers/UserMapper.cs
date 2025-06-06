using api.Dtos.Account;
using api.Models;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this AppUser user)
        {
            return new UserDto
            {
                Name = user.UserName,
                Email = user.Email,
                Tickets = user.Tickets
            .Where(t => t.Event != null) // Pomijamy bilety bez przypisanego eventu
            .Select(t => new UserTicketDto
            {
                Id = t.Id,          // ID biletu
                EventName = t.Event.Name  // Nazwa eventu
            })
            .ToList()
            };
        }
    }
}
