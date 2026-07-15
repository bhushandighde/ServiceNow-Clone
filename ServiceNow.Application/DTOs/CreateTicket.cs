using ServiceNow.ServiceNow.Domain.Enum;

namespace ServiceNow.ServiceNow.Application.DTOs
{
    public class CreateTicket
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TicketStatus Status { get; set; }

        public Priority Priority { get; set; }

        public int CreatedBy { get; set; }


    }
}
