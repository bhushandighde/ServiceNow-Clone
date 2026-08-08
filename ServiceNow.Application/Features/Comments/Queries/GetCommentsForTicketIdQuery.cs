using MediatR;
using ServiceNow.ServiceNow.Application.DTOs;

namespace ServiceNow.ServiceNow.Application.Features.Comments.Queries
{

    public record GetCommentsForTicketIdQuery(int TicketId) : IRequest<List<CreateCommentResponse>>;
    
}
