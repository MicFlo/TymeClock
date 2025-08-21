using System.ComponentModel.DataAnnotations;
using TimeClock.Core.Models;

namespace TimeClock.Core.DTOs;

public class CreateTimePunchDto
{
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    public PunchType Type { get; set; }
    
    public string? Notes { get; set; }
}

public class UpdateTimePunchDto
{
    [Required]
    public PunchType Type { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    public string? Notes { get; set; }
}

public class TimePunchResponseDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public PunchType Type { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
