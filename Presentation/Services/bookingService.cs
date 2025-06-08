using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

// Service for the booking database handling
namespace Presentation.Services {
    public class BookingService {
        private readonly DataContext _context;

        public BookingService(DataContext context) {
            _context = context;
        }

        public async Task<IEnumerable<bookingEntity>> GetAllAsync() {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<bookingEntity?> GetAsync(string eventId) {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == eventId);
        }

        //public async Task<bool> AddBookingAsync(bookingEntity booking) {
        //    _context.Bookings.Add(booking);
        //    return await _context.SaveChangesAsync() > 0;
        //}

        // Add booking, creates a new GUID incase a booking already exists with it.
        public async Task<bool> AddBookingAsync(bookingEntity booking) {
            if (await _context.Bookings.AnyAsync(b => b.Id == booking.Id)) {
                booking.Id = Guid.NewGuid().ToString(); // Ensure uniqueness
            }
            _context.Bookings.Add(booking);
            return await _context.SaveChangesAsync() > 0;
        }
        // Delete all bookings
        public async Task<bool> DeleteAllBookingsAsync() {
            _context.Bookings.RemoveRange(_context.Bookings);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}