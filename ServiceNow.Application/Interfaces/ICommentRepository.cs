using ServiceNow.ServiceNow.Domain.Entities;

namespace ServiceNow.ServiceNow.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<bool> AddComment(Comment comment);

        Task<List<Comment>> GetCommentsByTicketId(int ticketId);
    }
}
