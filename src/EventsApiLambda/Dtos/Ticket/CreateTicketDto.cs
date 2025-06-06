using api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Ticket
{
    public class CreateTicketDto
    {
        public string Type { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int EventId { get; set; }
        public string Owner { get; set; } = string.Empty;
       

    }
}
