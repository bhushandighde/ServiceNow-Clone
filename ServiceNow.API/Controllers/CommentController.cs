using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Features.Comments.Commands.CreateComment;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Features.Comments;

using ServiceNow.ServiceNow.Application.Features.Comments.Queries;

namespace ServiceNow.ServiceNow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("createComment")]
        public async Task<IActionResult> CreateTickets(CreateCommentRequest commentData)
        {
            if (commentData == null)
            {
                return BadRequest();
            }

            var command = new CreateCommentCommand(
                commentData.TicketId,
                commentData.Text,
                commentData.userId,
                commentData.CreatedAt
            );

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("getCommentsForTicketId/{TicketId}")]

        public async Task<IActionResult> GetCommentsForTicketId( int TicketId)
        {

            if (TicketId == null)
            {
                return BadRequest();
            }

            var query = new GetCommentsForTicketIdQuery( TicketId);

            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
