using MediatR;
using ServiceNow.ServiceNow.Application.Interfaces;

namespace ServiceNow.ServiceNow.Application.Features.AI.Ticket_Summary.Queries
{
    public class GetTicketSummaryHandler
         : IRequestHandler<GetTicketSummaryQuery, string>
    {
        private readonly ITicketRespository _ticketRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IAiService _aiService;

        public GetTicketSummaryHandler(
            ITicketRespository ticketRepository,
            ICommentRepository commentRepository,
            IAiService aiService)
        {
            _ticketRepository = ticketRepository;
            _commentRepository = commentRepository;
            _aiService = aiService;
        }

        public async Task<string> Handle(
            GetTicketSummaryQuery request,
            CancellationToken cancellationToken)
        {
            // Get ticket
            var ticket = await _ticketRepository
                .GetTicketsByIdFromDB(request.TicketId);

            if (ticket == null)
            {
                throw new KeyNotFoundException(
                    $"Ticket {request.TicketId} not found.");
            }

            // Get comments
            var comments = await _commentRepository
                .GetCommentsByTicketId(request.TicketId);

            // Convert comments to simple strings
            var commentTexts = comments
                .Select(c => c.Text)
                .ToList();

            // Send ticket + discussion to AI
            var summary = await _aiService.GenerateTicketSummary(
                ticket.Title,
                ticket.Description,
                ticket.Status.ToString(),
                ticket.Priority.ToString(),
                commentTexts
            );

            return summary;

        }
    }
}
