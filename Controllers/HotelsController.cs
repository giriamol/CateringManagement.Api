using CateringManagement.Application.DTOs;
using CateringManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hotels = await _hotelService.GetAllAsync();

        return Ok(hotels);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var hotel =
            await _hotelService.GetByIdAsync(id);

        if (hotel == null)
        {
            return NotFound(new
            {
                message = "Hotel not found."
            });
        }

        return Ok(hotel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateHotelDto dto)
    {
        try
        {
            var hotel =
                await _hotelService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = hotel.HotelId },
                hotel);
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
        [FromBody] UpdateHotelDto dto)
    {
        var updated =
            await _hotelService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = "Hotel not found."
            });
        }

        return Ok(new
        {
            message = "Hotel updated successfully."
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted =
            await _hotelService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Hotel not found."
            });
        }

        return Ok(new
        {
            message = "Hotel deleted successfully."
        });
    }
}
}
