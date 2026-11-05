using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAssignmentDataController : ControllerBase
    {
        private readonly IHotelAssignmentDataService _service;

        public HotelAssignmentDataController( IHotelAssignmentDataService service)
        {
            _service = service;
        }
        [HttpGet("summaryReport")]
        public async Task<IActionResult> GetPaymentReportSummary([FromQuery] long hotelId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string? status = null)
        {
            if (hotelId <= 0)
            {
                return BadRequest(new { message = "A valid HotelId is required." });
            }

            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new { message = "FromDate cannot be greater than ToDate." });
            }

            try
            {
                var result = await _service.GetPaymentReportSummary(hotelId, fromDate, toDate, status);

                if (result == null || !result.Any())
                {
                    return NotFound(new { message = "Caterer payment summary not found." });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("AllReport")]
        public async Task<IActionResult> GetAllReportPaymentSummary([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string? status = null)
        {
            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new { message = "FromDate cannot be greater than ToDate." });
            }

            try
            {
                var result = await _service.GetAllReportPaymentSummary(fromDate, toDate, status);

                if (result == null || !result.Any())
                {
                    return NotFound(new { message = "Caterer payment summary not found." });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // GET: api/HotelAssignmentData?date=2026-10-20
        [HttpGet]
        public async Task<IActionResult> GetHotelAllocationDetails( [FromQuery] DateTime? date)
        {
            if (!date.HasValue || date.Value == default)
            {
                return BadRequest(new
                {
                    message = "A valid assignment date is required."
                });
            }

            var result = await _service.GetHotelAllocationDetailsByDateAsync(date.Value.Date);

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetPaymentSummary([FromQuery] long hotelId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (hotelId <= 0)
            {
                return BadRequest(new { message = "A valid VendorId is required." });
            }

            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new { message = "FromDate cannot be greater than ToDate." });
            }

            try
            {
                var result = await _service.GetPaymentSummaryAsync(hotelId, fromDate, toDate);

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
        public async Task<IActionResult> SavePayment(SaveHotelPaymentDto dto)
        {
            try
            {
                var result = await _service.SavePaymentAsync(dto);

                return StatusCode(StatusCodes.Status201Created, new { message = "Hotel payment saved successfully.",data = result  });
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
