namespace ServiceNow.ServiceNow.Application.Interfaces
{
    public interface IAiService
    {
        Task<string> GenerateTicketSummary(
            string title,
            string description,
            string status,
            string priority,
            List<string> comments);
    }
}
