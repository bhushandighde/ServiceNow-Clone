using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket;
using ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTicketById;
using ServiceNow.ServiceNow.Domain.Entities;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket;
namespace ServiceNow.ServiceNow.Application.Interfaces
{
    public interface ITicketRespository
    {
        public Task<List<Tickets>> GetTicketsFromDB();

        public Task<Tickets> GetTicketsByIdFromDB(int ticketid);

        public Task<bool> AddTickettoDB(Tickets ticketData);

        public Task<List<Tickets>> GetTicketsForPageFromDB(int page);

        public Task<List<Tickets>> SearchAsyncFromDB(string keyword);
        // Task GetTicketsByIdFromDB(GetTicketsByIdQuery request);

        public Task<bool> updateTicketInDb(UpdateTicketCommand request);

        public Task<bool> DeleteTicketById(int id);
    }
}
