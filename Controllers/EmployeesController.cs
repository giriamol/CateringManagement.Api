using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using CateringManagement.Domain.Entities;
using CateringManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var employees = await _employeeService.GetAllAsync(pageNumber,pageSize);

            return Ok(employees);
        }

        // GET: api/Employees/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var employee =
                await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new
                {
                    message = "Employee not found."
                });
            }

            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            try
            {
                var employee = await _employeeService.CreateAsync(dto);

                return CreatedAtAction( nameof(GetById), new { id = employee.EmployeeId }, employee);
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

        // PUT: api/Employees/1
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id,UpdateEmployeeDto dto)
        {
            try
            {
                var updated = await _employeeService.UpdateAsync(id, dto);

                if (!updated)
                {
                    return NotFound(new
                    {
                        message = "Employee not found."
                    });
                }

                return Ok(new
                {
                    message = "Employee updated successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/Employees/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _employeeService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Employee not found."
                });
            }

            return Ok(new
            {
                message = "Employee deleted successfully."
            });
        }
    }
}
