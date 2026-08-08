namespace ServiceNow.ServiceNow.Application.DTOs
{
    public class CreateCommentRequest
    {
    
        public int TicketId { get; set; }
        public string Text { get; set; } = string.Empty;
        public int userId { get; set; }

        public DateTime CreatedAt { get; set; }



    }
}
