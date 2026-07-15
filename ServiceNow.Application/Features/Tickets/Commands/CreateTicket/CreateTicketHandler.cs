using MediatR;
using ServiceNow.ServiceNow.Infrastructure.Repositories;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Domain.Enum;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;
using AutoMapper;


namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketHandler
        : IRequestHandler<CreateTicketCommand, bool>
    {
        private readonly ITicketRespository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTicketHandler> _logger;

        public CreateTicketHandler(ITicketRespository ticketRepository, IMapper mapper, ILogger<CreateTicketHandler> logger)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "Creating ticket with title '{Title}' for user {UserId}",
            request.Title,
            request.CreatedBy);

            var ticket = _mapper.Map<ServiceNow.Domain.Entities.Tickets>(request) ;


            var result = await _ticketRepository.AddTickettoDB(ticket);

            if (result)
            {
                _logger.LogInformation(
                    "Ticket '{Title}' created successfully.",
                    request.Title);
            }
            else
            {
                _logger.LogWarning(
                    "Failed to create ticket '{Title}'.",
                    request.Title);
            }

            return result;

        }
    }
}