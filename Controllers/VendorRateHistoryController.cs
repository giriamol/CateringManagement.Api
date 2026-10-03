using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorRateHistoryController : ControllerBase
    {
        private readonly IVendorRateHistoryService _service;

        public VendorRateHistoryController(IVendorRateHistoryService service)
        {
            _service = service;
        }

        [HttpGet("vendor/{vendorId}")]
        public async Task<IActionResult> GetByVendor(
            long vendorId)
        {
            var result =
                await _service.GetByVendorIdAsync(vendorId);

            return Ok(result);
        }

        [HttpGet("vendor/{vendorId}/date/{date}")]
        public async Task<IActionResult> GetRateForDate(
            long vendorId,
            DateTime date)
        {
            var result =  await _service.GetRateForDateAsync( vendorId,date);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "No vendor rate found for the selected date."
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateVendorRateDto dto)
        {
            try
            {
                var result =
                    await _service.CreateAsync(dto);

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