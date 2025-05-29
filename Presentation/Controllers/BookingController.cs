using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

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
        public async Task<IActionResult> Get(string id) { // Corrected method name
            var result = await _bookingService.GetAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] bookingEntity booking) {
            var result = await _bookingService.AddBookingAsync(booking); // Corrected method name
            return result ? Ok() : BadRequest();
        }
    }
}
