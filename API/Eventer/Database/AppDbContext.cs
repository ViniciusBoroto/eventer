using Microsoft.EntityFrameworkCore;
using Eventer.Database.Schemas;

namespace Eventer.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");

            modelBuilder.Entity<Event>()
                .Property(e => e.PictureUrl)
                .IsRequired();

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Orders)
                .WithOne(o => o.Event)
                .HasForeignKey(o => o.EventId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Ticket)
                .WithOne(t => t.Order)
                .HasForeignKey<Order>(o => o.TicketId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany()
                .HasForeignKey(t => t.EventId);
        }
    }
}
