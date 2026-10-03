using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyHotelAssignmentController : ControllerBase
    {
        private readonly IDailyHotelAssignmentService _service;

        public DailyHotelAssignmentController(
            IDailyHotelAssignmentService service)
        {
            _service = service;
        }

        // GET: api/DailyHotelAssignment/date/2026-09-19
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var result = await _service.GetByDateAsync(date);

            return Ok(result);
        }

        // GET: api/DailyHotelAssignment/employee/1/date/2026-09-19
        [HttpGet("employee/{employeeId}/date/{date}")]
        public async Task<IActionResult> GetByEmployeeAndDate( long employeeId,   DateTime date)
        {
            var result = await _service.GetByEmployeeAndDateAsync(employeeId, date);

            return Ok(result);
        }

        // POST: api/DailyHotelAssignment
        [HttpPost]
        public async Task<IActionResult> Create( CreateDailyHotelAssignmentDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);

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
    }
}
