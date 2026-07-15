    using MediatR;
    using ServiceNow.ServiceNow.Application.DTOs;

    namespace ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTickets
    {
        public record GetTicketsQuery() : IRequest<List<TicketResponseDto>>;
    }

