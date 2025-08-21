using Microsoft.EntityFrameworkCore;
using TimeClock.Core.DTOs;
using TimeClock.Core.Interfaces;
using TimeClock.Core.Models;
using TimeClock.Infrastructure.Data;

namespace TimeClock.Infrastructure.Services;

public class TimePunchService : ITimePunchService
{
    private readonly TimeClockContext _context;

    public TimePunchService(TimeClockContext context)
    {
        _context = context;
    }

    public async Task<TimePunchResponseDto> CreateTimePunchAsync(CreateTimePunchDto createDto)
    {
        var timePunch = new TimePunch
        {
            UserId = createDto.UserId,
            Type = createDto.Type,
            Timestamp = DateTime.UtcNow,
            Notes = createDto.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.TimePunches.Add(timePunch);
        await _context.SaveChangesAsync();

        return MapToResponseDto(timePunch);
    }

    public async Task<IEnumerable<TimePunchResponseDto>> GetTimePunchesByUserAsync(string userId)
    {
        var timePunches = await _context.TimePunches
            .Where(tp => tp.UserId == userId)
            .OrderByDescending(tp => tp.Timestamp)
            .ToListAsync();

        return timePunches.Select(MapToResponseDto);
    }

    public async Task<TimePunchResponseDto?> GetTimePunchByIdAsync(int id)
    {
        var timePunch = await _context.TimePunches.FindAsync(id);
        return timePunch != null ? MapToResponseDto(timePunch) : null;
    }

    public async Task<TimePunchResponseDto> UpdateTimePunchAsync(int id, UpdateTimePunchDto updateDto)
    {
        var timePunch = await _context.TimePunches.FindAsync(id);
        if (timePunch == null)
            throw new InvalidOperationException($"TimePunch with ID {id} not found");

        timePunch.Type = updateDto.Type;
        timePunch.Timestamp = updateDto.Timestamp;
        timePunch.Notes = updateDto.Notes;
        timePunch.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponseDto(timePunch);
    }

    public async Task<bool> DeleteTimePunchAsync(int id)
    {
        var timePunch = await _context.TimePunches.FindAsync(id);
        if (timePunch == null)
            return false;

        _context.TimePunches.Remove(timePunch);
        await _context.SaveChangesAsync();
        return true;
    }

    private static TimePunchResponseDto MapToResponseDto(TimePunch timePunch)
    {
        return new TimePunchResponseDto
        {
            Id = timePunch.Id,
            UserId = timePunch.UserId,
            Type = timePunch.Type,
            Timestamp = timePunch.Timestamp,
            Notes = timePunch.Notes,
            CreatedAt = timePunch.CreatedAt,
            UpdatedAt = timePunch.UpdatedAt
        };
    }
}
