namespace api.Dtos.Account
{
    using api.Dtos.Ticket;
    using api.Models;

    public class UserDto
    {
        public List<UserTicketDto> Tickets { get; set; } = new List<UserTicketDto>();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    } 
}
