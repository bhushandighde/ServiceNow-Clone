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

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tickets>()
    .HasOne(t => t.AssignedUser)
    .WithMany()
    .HasForeignKey(t => t.AssignedTo)
    .OnDelete(DeleteBehavior.SetNull);


        }
    }
}