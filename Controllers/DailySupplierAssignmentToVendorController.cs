using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailySupplierAssignmentToVendorController : ControllerBase
    {
        private readonly IDailySupplierAssignmentToVendorService _service;

        public DailySupplierAssignmentToVendorController(IDailySupplierAssignmentToVendorService service)
        {
            _service = service;
        }

        // GET: api/DailySupplierAssignmentToVendor
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        // GET: api/DailySupplierAssignmentToVendor/2026-11-05
        [HttpGet("{date}")]
        public async Task<IActionResult> GetByRangeDate(DateTime date)
        {
            var result = await _service.GetByDateRangeAsync(date);

            if (result == null)
                return NotFound(new
                {
                    message = "Assignment not found."
                });

            return Ok(result);
        }

        // GET: api/DailySupplierAssignmentToVendor/date/2026-11-02
        [HttpGet("getbydate")]
        public async Task<IActionResult> GetByDate([FromQuery] long vendorId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
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
                    var result = await _service.GetByDateAsync(vendorId, fromDate, toDate);

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
            public async Task<IActionResult> GetAllSupplierSummary([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
            {
                if (fromDate.Date > toDate.Date)
                {
                    return BadRequest(new { message = "FromDate cannot be greater than ToDate." });
                }

                try
                {
                    var result = await _service.GetAllSupplierSummaryAsync(fromDate, toDate);

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



            //-------------------------------------------
            // POST: api/DailySupplierAssignmentToVendor
            [HttpPost]
        public async Task<IActionResult> Add([FromBody] DailySupplierAssignmentToVendorDto dto)
        {
            try
            {
                var result = await _service.AddAsync(dto);

                return CreatedAtAction(
                    nameof(Add),
                    new { id = result.SupplierAssignmentId },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/DailySupplierAssignmentToVendor/5
        //[HttpPut("{id:long}")]
        //public async Task<IActionResult> Update( long id, [FromBody] DailySupplierAssignmentToVendorDto dto)
        //{
        //    try
        //    {
        //        var result = await _service.UpdateAsync(id, dto);

        //        if (!result)
        //            return NotFound(new
        //            {
        //                message = "Assignment not found."
        //            });

        //        return Ok(new
        //        {
        //            message = "Assignment updated successfully."
        //        });
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        // DELETE: api/DailySupplierAssignmentToVendor/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound(new
                {
                    message = "Assignment not found."
                });

            return Ok(new
            {
                message = "Assignment deleted successfully."
            });
        }
    }
}
