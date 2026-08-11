
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetTicketsByIdHandler(ITicketRespository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<TicketResponseDto> Handle(GetTicketsByIdQuery request, CancellationToken cancellationToken)
        {
            var t = await _ticketRepository.GetTicketsByIdFromDB(request.TicketId);
            if (t == null)
                throw new KeyNotFoundException($"Ticket {request.TicketId} not found.");


            Console.WriteLine($"AssignedTo: {t?.AssignedTo}");
            Console.WriteLine($"AssignedUser: {t?.AssignedUser?.Name}");

            return _mapper.Map<TicketResponseDto>(t);

        }
    }
    }
