using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePaymentsController : ControllerBase
    {
        private readonly IEmployeePaymentService _service;

        public EmployeePaymentsController( IEmployeePaymentService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetPaymentSummary( [FromQuery] long employeeId,  [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (employeeId <= 0)
            {
                return BadRequest(new
                {
                    message = "A valid EmployeeId is required."
                });
            }

            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new
                {
                    message = "FromDate cannot be greater than ToDate."
                });
            }

            try
            {
                var result = await _service.GetPaymentSummaryAsync( employeeId, fromDate, toDate);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Employee payment summary not found."
                    });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllPaymentSummary( [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            
            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new
                {
                    message = "FromDate cannot be greater than ToDate."
                });
            }

            try
            {
                var result = await _service.GetAllPaymentSummaryAsync(fromDate, toDate);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Employee payment summary not found."
                    });
                }

                return Ok(result);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePayment([FromBody] SaveEmployeePaymentDto dto)
        {
            try
            {
                var result = await _service.SavePaymentAsync(dto);

                return StatusCode(
                    StatusCodes.Status201Created,
                    new
                    {
                        message = "Employee payment saved successfully.",
                        data = result
                    });
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
