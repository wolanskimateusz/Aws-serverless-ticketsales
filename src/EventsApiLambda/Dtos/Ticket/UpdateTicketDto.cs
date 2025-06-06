using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Ticket
{
    public class UpdateTicketDto
    {
        public string Type { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string EventName { get; set; } = string.Empty;

    }
}
