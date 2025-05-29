using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Services {
    public class BookingService {
        private readonly DataContext _context;

        public BookingService(DataContext context) {
            _context = context;
        }

        // Get all bookings
        public async Task<IEnumerable<bookingEntity>> GetAllAsync() {
            return await _context.Bookings.ToListAsync();
        }

        // Get a single booking by eventId
        public async Task<bookingEntity?> GetAsync(string eventId) {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == eventId); // Fix case & property name
        }

        public async Task<bool> AddBookingAsync(bookingEntity booking) {
            _context.Bookings.Add(booking);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
