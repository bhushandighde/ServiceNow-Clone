namespace ServiceNow.ServiceNow.Application.DTOs
{
    public class CreateCommentResponse
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
