using Microsoft.EntityFrameworkCore;
using ServiceNow.Infrastructure.Persistence;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;

namespace ServiceNow.ServiceNow.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddComment(Comment comment)
        {
            var response = await _dbContext.Comments.AddAsync(comment);

            return await _dbContext.SaveChangesAsync() > 0;


        }
        public async Task<List<Comment>> GetCommentsByTicketId(int ticketId)
        {
            var comments = await _dbContext.Comments.Where(c => c.TicketId == ticketId).ToListAsync();
            return comments;


        }
    }
}
