using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Application.Features.AI.Ticket_Summary.Queries;

namespace ServiceNow.ServiceNow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ticket/{ticketId}/summary")]
        public async Task<IActionResult> GetTicketSummary(int ticketId)
        {
            var query = new GetTicketSummaryQuery(ticketId);

            var response = await _mediator.Send(query);

            return Ok(new
            {
                summary = response
            });
        }
    }
}
