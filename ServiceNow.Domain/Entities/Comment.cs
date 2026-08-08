using System.Net.Sockets;
using ServiceNow.ServiceNow.Domain.Entities;

namespace ServiceNow.ServiceNow.Domain.Entities
{
    public class Comment
    {

        public int Id { get; set; }

        public int TicketId { get; set; }

        public string Text { get; set; } = string.Empty;

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Tickets Ticket { get; set; } = null!;

    }
}
