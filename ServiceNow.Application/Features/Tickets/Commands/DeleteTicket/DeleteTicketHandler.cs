using MediatR;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Infrastructure.Repositories;
using System.Reflection.Metadata;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand, bool>
    {
        public readonly ITicketRespository _ticketRespository;
        public DeleteTicketHandler(ITicketRespository ticketRepository)
        {
            _ticketRespository = ticketRepository;
        }

       public async Task<bool> Handle(DeleteTicketCommand command, CancellationToken cancellation)
        {
            bool IsSuccessfull = await _ticketRespository.DeleteTicketById(command.Id);

            return true;

        }
    }
}
