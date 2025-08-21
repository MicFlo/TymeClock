using TimeClock.Core.DTOs;
using TimeClock.Core.Models;

namespace TimeClock.Core.Interfaces;

public interface ITimePunchService
{
    Task<TimePunchResponseDto> CreateTimePunchAsync(CreateTimePunchDto createDto);
    Task<IEnumerable<TimePunchResponseDto>> GetTimePunchesByUserAsync(string userId);
    Task<TimePunchResponseDto?> GetTimePunchByIdAsync(int id);
    Task<TimePunchResponseDto> UpdateTimePunchAsync(int id, UpdateTimePunchDto updateDto);
    Task<bool> DeleteTimePunchAsync(int id);
}
