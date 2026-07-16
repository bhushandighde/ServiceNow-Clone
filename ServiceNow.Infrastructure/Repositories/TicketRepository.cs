using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceNow.Infrastructure.Persistence;
using ServiceNow.ServiceNow.Domain.Entities;
using ServiceNow.ServiceNow.Application.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket;
using ServiceNow.ServiceNow.Application.Interfaces;
using AutoMapper;

namespace ServiceNow.ServiceNow.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRespository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public TicketRepository(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<Tickets>> GetTicketsFromDB()
        {
            return await _db.Tickets.AsNoTracking().ToListAsync();

        }

        public async Task<Tickets> GetTicketsByIdFromDB(int ticketid)
        {
            return await _db.Tickets.AsNoTracking().FirstOrDefaultAsync(u => u.Id == ticketid);
        }
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
        public async Task<bool> AddTickettoDB(Tickets ticketData)
        {
            
            ticketData.CreatedAt = GetDateTime();

            _db.Tickets.Add(ticketData);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> updateTicketInDb(UpdateTicketCommand request)
        {
            var ticket  = await _db.Tickets.FirstOrDefaultAsync(u => u.Id == request.Id);

            _mapper.Map(request,ticket);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<List<Tickets>> GetTicketsForPageFromDB(int page)
        {
            var tickets = await _db.Tickets
                         .OrderByDescending(t => t.Id)
                         .Skip(page)
                         .Take(10)
                         .ToListAsync();

            return tickets;
        }

        public async Task<bool> DeleteTicketById(int id)
        {
            int res = await _db.Tickets.Where(t => t.Id == id).ExecuteDeleteAsync();
            _db.SaveChanges();
            return res > 0;

        }
        public async Task<List<Tickets>> SearchAsyncFromDB(string keyword)
        {
           var tickets= _db.Tickets.Where(x => 
             x.Title.Contains(keyword) ||
                x.Description.Contains(keyword));
           return tickets.ToList(); 
        }
    }
}
