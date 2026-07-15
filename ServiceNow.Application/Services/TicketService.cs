using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;

namespace ServiceNow.ServiceNow.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRespository _ticketRepository;

        public TicketService(ITicketRespository ticketRepository)
        {
            _ticketRepository = ticketRepository;

        }

        public async Task<List<Tickets>> GetTicketsAsync()
        {
            var tickets = await _ticketRepository.GetTicketsFromDB();
            return tickets;
        }

        public async Task<Tickets> GetTicketByIdAsync(int ticketid)
        {
            Tickets ticket = await _ticketRepository.GetTicketsByIdFromDB(ticketid);
            return ticket;
        }
        public async Task<bool> CreateTicketAsync(Tickets ticketData)
        {
            if (ticketData == null) throw new ArgumentNullException(nameof(ticketData));
            if (ticketData.CreatedBy == null)
            {
                throw new Exception("To create Ticket User should be logged in. No User id found");
            }

            var response = await _ticketRepository.AddTickettoDB(ticketData);
            return response;
        }

        public async Task<List<Tickets>> GetTicketForPageAsync(int page)
        {
            int offset = (page - 1) * 10;
            var tickets = await _ticketRepository.GetTicketsForPageFromDB(offset);
            return tickets;

        }

        public  async Task<List<Tickets>> SearchAsync(string keyword)
        {
            List < Tickets> result = await _ticketRepository.SearchAsyncFromDB(keyword);
            return result;

        }
    }
}
