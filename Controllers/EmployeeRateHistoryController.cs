using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using CateringManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRateHistoryController : ControllerBase
    {
        private readonly IEmployeeRateHistoryService _service;

        public EmployeeRateHistoryController(IEmployeeRateHistoryService service)
        {
            _service = service;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllPagedAsync();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                return BadRequest(new
                {
                    message = "Page number must be greater than 0."
                });
            }

            if (pageSize < 1 || pageSize > 100)
            {
                return BadRequest(new
                {
                    message = "Page size must be between 1 and 100."
                });
            }

            var result = await _service.GetAllPagedAsync(
                pageNumber,
                pageSize
            );

            return Ok(result);
        }

        [HttpGet("employee/{EmployeeRateHistoryId}")]
        public async Task<IActionResult> GetByEmployeeID(long EmployeeRateHistoryId)
        {
            var result = await _service.GetByEmployeeIdAsync(EmployeeRateHistoryId);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeRateDto dto)
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
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(long id, EmployeeRateDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);

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
