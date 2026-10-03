using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await _vendorService.GetAllAsync();

            return Ok(vendors);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var vendor =
                await _vendorService.GetByIdAsync(id);

            if (vendor == null)
            {
                return NotFound(new
                {
                    message = "Vendor not found."
                });
            }

            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateVendorDto dto)
        {
            try
            {
                var vendor =
                    await _vendorService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = vendor.VendorId },
                    vendor);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateVendorDto dto)
        {
            var updated =
                await _vendorService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    message = "Vendor not found."
                });
            }

            return Ok(new
            {
                message = "Vendor updated successfully."
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted =
                await _vendorService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Vendor not found."
                });
            }

            return Ok(new
            {
                message = "Vendor deleted successfully."
            });
        }
    }
}