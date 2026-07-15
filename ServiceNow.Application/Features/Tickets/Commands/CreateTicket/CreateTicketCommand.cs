using MediatR;
using ServiceNow.ServiceNow.Domain.Enum;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(
        string Title,
        string Description,
        TicketStatus Status,
        Priority Priority,
        int CreatedBy
    ) : IRequest<bool>;
}

 