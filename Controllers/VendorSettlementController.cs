using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorSettlementController : ControllerBase
    {
        private readonly IVendorSettlementService _service;

        public VendorSettlementController(
            IVendorSettlementService service)
        {
            _service = service;
        }

        // POST: api/VendorSettlement/calculate
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(
            CreateVendorSettlementDto dto)
        {
            try
            {
                var result = await _service.CalculateAsync(dto);

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

        // GET:
        // api/VendorSettlement/vendor/1/year/2026/month/9
        [HttpGet("vendor/{vendorId}/year/{year}/month/{month}")]
        public async Task<IActionResult> GetByVendorAndMonth(
            long vendorId,
            int year,
            int month)
        {
            var result = await _service
                .GetByVendorAndMonthAsync(
                    vendorId,
                    year,
                    month);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Vendor settlement not found."
                });
            }

            return Ok(result);
        }

        // GET:
        // api/VendorSettlement/1
        [HttpGet("{settlementId:long}")]
        public async Task<IActionResult> GetById(
            long settlementId)
        {
            var result = await _service
                .GetByIdAsync(settlementId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Vendor settlement not found."
                });
            }

            return Ok(result);
        }
    }

}
