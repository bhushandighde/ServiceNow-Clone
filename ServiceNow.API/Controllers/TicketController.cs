using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Domain.Entities;
using ServiceNow.ServiceNow.Application.DTOs;
using System.Net;
using MediatR;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket;
using ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTickets;
using Microsoft.AspNetCore.Http.Features;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTicketById;
using Microsoft.EntityFrameworkCore.Update.Internal;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.DeleteTicket;
namespace ServiceNow.ServiceNow.API.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _iTicketService;
        private readonly IMediator _mediator;

        public TicketController(ITicketService iTicketService, IMediator mediator)
        {
            _iTicketService = iTicketService;
            _mediator = mediator;
        }


        [HttpGet("getTickets")]
        public async Task<IActionResult> GetTickets()
        {
            List<TicketResponseDto> tickets = await _mediator.Send(new GetTicketsQuery());
            return Ok(tickets);

        }

        [HttpGet("{ticketid}")]
        public async Task<TicketResponseDto> GetTicketsById(int ticketid)
        {
            TicketResponseDto ticket = await _mediator.Send(new GetTicketsByIdQuery{ TicketId = ticketid});

            return ticket;

        }

        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTickets(CreateTicket ticketData)
        {
            var command = new CreateTicketCommand(
                                ticketData.Title,
                                ticketData.Description,
                                ticketData.Status,
                                ticketData.Priority,
                                ticketData.CreatedBy
                               );

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("deleteTicket")]

        public async Task<IActionResult> DeleteTicket(int Id)
        {
            var command = new DeleteTicketCommand(Id);
            var result = await _mediator.Send(command);

            return Ok();
        }
            public async Task<IActionResult> UpdateTickets(UpdateTicket ticketData)
        {
            var command = new UpdateTicketCommand(
                               ticketData.Id,
                               ticketData.Title,
                               ticketData.Description,
                               ticketData.Status,
                               ticketData.Priority
                              );

            var response = await _mediator.Send(command);

            return Ok();

        }

        [HttpGet("page/{page}")]
        public async Task<List<Tickets>> GetTicketsForPage(int page)
        {
            List<Tickets> ticket = await _iTicketService.GetTicketForPageAsync(page);
            return ticket;

        }

        [HttpGet("search")]
        public async Task<List<Tickets>> Search(string keyword)
        {
            List <Tickets> result = await _iTicketService.SearchAsync(keyword);
            return result;

        }
    }
}
