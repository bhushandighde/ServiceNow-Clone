using MediatR;
using ServiceNow.ServiceNow.Application.DTOs;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTicketById
{
    public class GetTicketsByIdQuery : IRequest<TicketResponseDto>
    {
        public int TicketId { get; set; }

     
    };
   
}
