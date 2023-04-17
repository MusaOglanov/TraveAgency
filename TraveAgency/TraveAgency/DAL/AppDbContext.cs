using Microsoft.EntityFrameworkCore;
using TraveAgency.Models;

namespace TraveAgency.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<HotelDetail> HotelDetails { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<HotelRoomType> HotelRoomTypes { get; set; }
        public DbSet<AirlineTicket> AirlineTickets { get; set; }
        public DbSet<AirlineTicketDetail> AirlineTicketDetails { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<SeatClass> SeatClasses { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<TourHotel> TourHotels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .Property(h => h.Country)
                .HasDefaultValue("Azərbaycan");
            

            modelBuilder.Entity<AirlineTicket>()
              .HasOne(t => t.DepartureAirport)
              .WithMany()
              .HasForeignKey(t => t.DepartureAirportId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirlineTicket>()
                .HasOne(t => t.ArrivalAirport)
                .WithMany()
                .HasForeignKey(t => t.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirlineTicket>()
               .HasOne(t => t.TransferAirport)
               .WithMany()
               .HasForeignKey(t => t.TransferAirportId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirlineTicket>()
                .HasOne(t => t.ReturnAirport)
                .WithMany()
                .HasForeignKey(t => t.ReturnAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
