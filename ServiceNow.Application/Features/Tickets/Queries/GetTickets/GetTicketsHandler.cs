using AutoMapper;
using MediatR;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Infrastructure.Repositories;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTickets
{
    public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, List<TicketResponseDto>>
    {
        private readonly ITicketRespository _ticketRepository;
        private readonly IMapper _mapper;


        public GetTicketsHandler(ITicketRespository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<List<TicketResponseDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetTicketsFromDB();

            return _mapper.Map<List<TicketResponseDto>>(tickets);

        }
    }
}
