using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAdvanceController : ControllerBase
    {
        private readonly IEmployeeAdvanceService _service;

        public EmployeeAdvanceController(
            IEmployeeAdvanceService service)
        {
            _service = service;
        }


        // =========================================================
        // GET ALL
        // GET: api/EmployeeAdvance
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    });
            }
        }


        // =========================================================
        // GET BY ID
        // GET: api/EmployeeAdvance/1
        // =========================================================

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Employee advance not found."
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    });
            }
        }


        // =========================================================
        // CREATE
        // POST: api/EmployeeAdvance
        // =========================================================

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateEmployeeAdvanceDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new
                    {
                        id = result!.AdvanceId
                    },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    });
            }
        }


        // =========================================================
        // UPDATE
        // PUT: api/EmployeeAdvance/1
        // =========================================================

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateEmployeeAdvanceDto dto)
        {
            try
            {
                var result =
                    await _service.UpdateAsync(id, dto);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Employee advance not found."
                    });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    });
            }
        }


        // =========================================================
        // DELETE
        // DELETE: api/EmployeeAdvance/1
        // =========================================================

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result =
                    await _service.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new
                    {
                        message = "Employee advance not found."
                    });
                }

                return Ok(new
                {
                    message =
                        "Employee advance deleted successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    });
            }
        }
    }
}