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

        [HttpGet]
        public async Task<IActionResult> GetAll(
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result =
                await _service.GetAllAsync(
                    pageNumber,
                    pageSize
                );

            return Ok(result);
        }

        [HttpGet("vendor/{VendorRateHistoryId}")]
        public async Task<IActionResult> GetByVendor( long VendorRateHistoryId)
        {
            var result =
                await _service.GetByVendorIdAsync(VendorRateHistoryId);

            return Ok(result);
        }

        [HttpGet("vendor/{vendorId}/date/{date}")]
        public async Task<IActionResult> GetRateForDate( long vendorId, DateTime date)
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
        public async Task<IActionResult> Create( CreateVendorRateDto dto)
        {
            try
            {
                var result =  await _service.CreateAsync(dto);

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
        [HttpPut("{vendorRateHistoryId:long}")]
        public async Task<IActionResult> Update( long vendorRateHistoryId, [FromBody] UpdateVendorRateDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(
                    vendorRateHistoryId, dto);

                if (!updated)
                {
                    return NotFound(new
                    {
                        message = "Vendor rate history record not found."
                    });
                }

                return Ok(new
                {
                    message = "Vendor rate updated successfully."
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

        [HttpDelete("{vendorRateHistoryId:long}")]
        public async Task<IActionResult> Delete(
            long vendorRateHistoryId)
        {
            try
            {
                var deleted = await _service.DeleteAsync(
                    vendorRateHistoryId);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        message = "Vendor rate history record not found."
                    });
                }

                return Ok(new
                {
                    message = "Vendor rate deleted successfully."
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
    }
}