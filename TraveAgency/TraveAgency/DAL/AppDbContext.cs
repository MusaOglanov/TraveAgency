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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .Property(h => h.Country)
                .HasDefaultValue("Azərbaycan");
        }
    }
}
