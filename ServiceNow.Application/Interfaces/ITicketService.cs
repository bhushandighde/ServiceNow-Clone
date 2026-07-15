using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Domain.Entities;
using ServiceNow.ServiceNow.Application.DTOs;

namespace ServiceNow.ServiceNow.Application.Interfaces
{
    public interface ITicketService
    {
        public Task<List<Tickets>> GetTicketsAsync();
        public Task<Tickets> GetTicketByIdAsync(int ticketid);
        public Task<bool> CreateTicketAsync(Tickets ticketData);
        public Task<List<Tickets>> GetTicketForPageAsync(int page);
        public Task<List<Tickets>> SearchAsync(string search);
    }
}
