using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBillingController : ControllerBase
    {
        private readonly IHotelBillingService _service;

        public HotelBillingController(IHotelBillingService service)
        {
            _service = service;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateBill( CreateHotelBillDto dto)
        {
            try
            {
                var result = await _service.GenerateBillAsync(dto);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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

        [HttpGet("{hotelBillId:long}")]
        public async Task<IActionResult> GetById(
            long hotelBillId)
        {
            var result = await _service.GetByIdAsync(hotelBillId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Hotel bill not found."
                });
            }

            return Ok(result);
        }
    }
}

