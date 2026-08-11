using MediatR;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketHandler : IRequestHandler<UpdateTicketCommand, bool>
    {
        private readonly ITicketRespository _ticketRespository;

        public UpdateTicketHandler(ITicketRespository ticketRespository)
        {
            _ticketRespository = ticketRespository;
        }

        public async Task<bool>  Handle( UpdateTicketCommand request, CancellationToken cancellationToken)
        {

            var ticket = new DTOs.UpdateTicket
            {   
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
               AssignedTo = request.AssignedTo
            };
            await _ticketRespository.updateTicketInDb(request);

            return true;
        }
    }
}
