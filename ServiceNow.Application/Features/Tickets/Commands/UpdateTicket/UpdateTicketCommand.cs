using MediatR;
using ServiceNow.ServiceNow.Domain.Enum;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket
{
    public record UpdateTicketCommand(
        int Id,
        string Title,
        string Description,
        TicketStatus Status,
        Priority Priority,
        int? AssignedTo 
        ) : IRequest<bool>;
   
}
