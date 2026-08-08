using MediatR;
using ServiceNow.ServiceNow.Application.DTOs;
namespace ServiceNow.ServiceNow.Application.Features.Comments.Commands.CreateComment
{
  
        public record CreateCommentCommand(

            int Ticketid,
            string Text,
            int userId,
            DateTime CreatedAt
                    
        ) : IRequest<CreateCommentResponse>;



    
}
