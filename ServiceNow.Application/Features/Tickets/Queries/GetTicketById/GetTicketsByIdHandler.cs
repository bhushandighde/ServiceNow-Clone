
using MediatR;
using Microsoft.AspNetCore.Authentication;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Features.Tickets.Queries;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTicketById
{
    public class GetTicketsByIdHandler : IRequestHandler<GetTicketsByIdQuery, TicketResponseDto>
    {

        private readonly ITicketRespository _ticketRepository;


        public GetTicketsByIdHandler(ITicketRespository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketResponseDto> Handle(GetTicketsByIdQuery request, CancellationToken cancellationToken)
        {
            var t = await _ticketRepository.GetTicketsByIdFromDB(request.TicketId);

            return new TicketResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                CreatedAt = t.CreatedAt,
                CreatedBy = t.CreatedBy
            };
            
        }

    }
}
