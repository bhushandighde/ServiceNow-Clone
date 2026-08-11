using ServiceNow.ServiceNow.Domain.Enum;
namespace ServiceNow.ServiceNow.Domain.Entities
{
    public class Tickets
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TicketStatus Status { get; set; }

        public Priority Priority { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public int? AssignedTo { get; set; }

        public User? AssignedUser { get; set; }
    }

}
