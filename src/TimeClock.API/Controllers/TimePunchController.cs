using Microsoft.AspNetCore.Mvc;
using TimeClock.Core.DTOs;
using TimeClock.Core.Interfaces;

namespace TimeClock.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimePunchController : ControllerBase
{
    private readonly ITimePunchService _timePunchService;

    public TimePunchController(ITimePunchService timePunchService)
    {
        _timePunchService = timePunchService;
    }

    [HttpPost]
    public async Task<ActionResult<TimePunchResponseDto>> CreateTimePunch([FromBody] CreateTimePunchDto createDto)
    {
        try
        {
            var result = await _timePunchService.CreateTimePunchAsync(createDto);
            return CreatedAtAction(nameof(GetTimePunchById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TimePunchResponseDto>>> GetTimePunchesByUser(string userId)
    {
        try
        {
            var result = await _timePunchService.GetTimePunchesByUserAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TimePunchResponseDto>> GetTimePunchById(int id)
    {
        try
        {
            var result = await _timePunchService.GetTimePunchByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TimePunchResponseDto>> UpdateTimePunch(int id, [FromBody] UpdateTimePunchDto updateDto)
    {
        try
        {
            var result = await _timePunchService.UpdateTimePunchAsync(id, updateDto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTimePunch(int id)
    {
        try
        {
            var result = await _timePunchService.DeleteTimePunchAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
