using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateMastersController : ControllerBase
    {
        private readonly IRateMastersService _service;

        public RateMastersController( IRateMastersService service)
        {
            _service = service;
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotel(
            long hotelId)
        {
            var result =
                await _service.GetByHotelIdAsync(hotelId);

            return Ok(result);
        }

        [HttpGet("hotel/{hotelId}/date/{date}")]
        public async Task<IActionResult> GetRateForDate(
            long hotelId,
            DateTime date)
        {
            var result =
                await _service.GetRateForDateAsync(
                    hotelId,
                    date);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "No hotel rate found for the selected date."
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create( CreateHotelRateDto dto)
        {
            try
            {
                var result =  await _service.CreateAsync(dto);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
