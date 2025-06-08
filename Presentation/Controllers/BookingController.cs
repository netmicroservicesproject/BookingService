using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

// controller to get, add and delete bookings, copilot assisted
namespace Presentation.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService) {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var result = await _bookingService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            var result = await _bookingService.GetAsync(id);
            return result != null ? Ok(result) : NotFound();
        }
        // Add a booking that can be confirmed later (fetched from confirm microservice)
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] bookingEntity booking) {
            var result = await _bookingService.AddBookingAsync(booking);
            return result ? Ok() : BadRequest();
        }

        // Delete bookings (this is done after confirmation)
        [HttpDelete]
        public async Task<IActionResult> DeleteAllBookings() {
            var result = await _bookingService.DeleteAllBookingsAsync();
            return result ? Ok("All bookings deleted.") : BadRequest("Failed to delete bookings.");
        }
    }
}