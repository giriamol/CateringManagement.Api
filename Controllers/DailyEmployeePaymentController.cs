using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyEmployeePaymentController : ControllerBase
    {
        private readonly IDailyEmployeePaymentService _service;

        public DailyEmployeePaymentController(IDailyEmployeePaymentService service)
        {
            _service = service;
        }

        // POST: api/DailyEmployeePayment/calculate
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate( CreateDailyEmployeePaymentDto dto)
        {
            try
            {
                var result = await _service.CalculateAsync(dto);

                return Ok(result);
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

        // GET:
        // api/DailyEmployeePayment/employee/1/date/2026-09-19
        [HttpGet("employee/{employeeId}/date/{date}")]
        public async Task<IActionResult> GetByEmployeeAndDate(
            long employeeId,
            DateTime date)
        {
            var result = await _service
                .GetByEmployeeAndDateAsync(employeeId, date.Date);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Daily payment not found."
                });
            }

            return Ok(result);
        }

        // GET:
        // api/DailyEmployeePayment/1
        [HttpGet("{paymentId:long}")]
        public async Task<IActionResult> GetById(
            long paymentId)
        {
            var result = await _service.GetByIdAsync(paymentId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Daily payment not found."
                });
            }

            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllEmployeePayments( DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default || toDate == default)
            {
                return BadRequest(new
                {
                    message = "Both fromDate and toDate are required."
                });
            }

            if (fromDate.Date > toDate.Date)
            {
                return BadRequest(new
                {
                    message = "From date cannot be greater than To date."
                });
            }

            try
            {
                var result =
                    await _service.GetAllEmployeePaymentsAsync(
                        fromDate,
                        toDate);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }

}
