using MediatR;
namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.DeleteTicket
{
    public record DeleteTicketCommand
    (
        int Id
    ) : IRequest<bool>;
    
}
