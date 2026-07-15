using Microsoft.EntityFrameworkCore;
using ServiceNow.ServiceNow.Domain.Entities;
using System.Data.Common;
//using ServiceNow.Domain.Entities; // assuming Ticket is here

namespace ServiceNow.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Tickets> Tickets { get; set; }
    }
}