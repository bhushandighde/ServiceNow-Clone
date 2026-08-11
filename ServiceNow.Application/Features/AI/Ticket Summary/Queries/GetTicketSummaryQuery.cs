using MediatR;
namespace ServiceNow.ServiceNow.Application.Features.AI.Ticket_Summary.Queries

{
    public record GetTicketSummaryQuery(int TicketId) : IRequest<string>;

}
