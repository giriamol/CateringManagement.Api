using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelCollectionController : ControllerBase
    {
        private readonly IHotelCollectionService _service;

        public HotelCollectionController(
            IHotelCollectionService service)
        {
            _service = service;
        }

        // GET: api/HotelCollection
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result =
        //        await _service.GetAllAsync();

        //    return Ok(result);
        //}

        //// GET: api/HotelCollection/1
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var result =
        //        await _service.GetByIdAsync(id);

        //    if (result == null)
        //    {
        //        return NotFound(
        //            "Hotel collection not found.");
        //    }

        //    return Ok(result);
        //}

        //// POST: api/HotelCollection
        //[HttpPost]
        //public async Task<IActionResult> Create(
        //    [FromBody] CreateHotelCollectionDto dto)
        //{
        //    if (dto.HotelId <= 0)
        //    {
        //        return BadRequest(
        //            "Invalid HotelId.");
        //    }

        //    if (string.IsNullOrWhiteSpace(
        //        dto.CollectionName))
        //    {
        //        return BadRequest(
        //            "Collection name is required.");
        //    }

        //    if (dto.Amount < 0)
        //    {
        //        return BadRequest(
        //            "Amount cannot be negative.");
        //    }

        //    var result =
        //        await _service.CreateAsync(dto);

        //    return CreatedAtAction(
        //        nameof(GetById),
        //        new
        //        {
        //            id = result.HotelCollectionId
        //        },
        //        result);
        //}

        //// PUT: api/HotelCollection/1
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> Update(
        //    int id,
        //    [FromBody] UpdateHotelCollectionDto dto)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest(
        //            "Invalid collection ID.");
        //    }

        //    if (dto.Amount < 0)
        //    {
        //        return BadRequest(
        //            "Amount cannot be negative.");
        //    }

        //    var result =
        //        await _service.UpdateAsync(id, dto);

        //    if (!result)
        //    {
        //        return NotFound(
        //            "Hotel collection not found.");
        //    }

        //    return Ok(
        //        "Hotel collection updated successfully.");
        //}

        //// DELETE: api/HotelCollection/1
        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result =
        //        await _service.DeleteAsync(id);

        //    if (!result)
        //    {
        //        return NotFound(
        //            "Hotel collection not found.");
        //    }

        //    return Ok(
        //        "Hotel collection deleted successfully.");
        //}
    }
}
