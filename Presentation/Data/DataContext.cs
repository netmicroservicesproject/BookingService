using Microsoft.EntityFrameworkCore;

namespace Presentation.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<bookingEntity> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<bookingEntity>().ToTable("Bookings"); // Ensure correct table mapping
        }
    }
}