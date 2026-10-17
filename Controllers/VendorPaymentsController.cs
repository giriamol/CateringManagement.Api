using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorPaymentsController : ControllerBase
    {
        private readonly IVendorPaymentService _service;

        public VendorPaymentsController(IVendorPaymentService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetPaymentSummary([FromQuery] long vendorId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (vendorId <= 0)
            {
                return BadRequest(new { message = "A valid VendorId is required." });
            }

            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new { message = "FromDate cannot be greater than ToDate." });
            }

            try
            {
                var result = await _service.GetPaymentSummaryAsync(vendorId, fromDate, toDate);

                if (result == null)
                {
                    return NotFound(new { message = "Vendor payment summary not found." });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllPaymentSummary([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new { message = "FromDate cannot be greater than ToDate." });
            }

            try
            {
                var result = await _service.GetAllPaymentSummaryAsync(fromDate, toDate);

                if (result == null)
                {
                    return NotFound(new { message = "Vendor payment summary not found." });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePayment([FromBody] SaveVendorPaymentDto dto)
        {
            try
            {
                var result = await _service.SavePaymentAsync(dto);

                return StatusCode(StatusCodes.Status201Created, new { message = "Vendor payment saved successfully.", data = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
