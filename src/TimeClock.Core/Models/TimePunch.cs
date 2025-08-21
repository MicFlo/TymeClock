using System.ComponentModel.DataAnnotations;

namespace TimeClock.Core.Models;

public class TimePunch
{
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    public PunchType Type { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}

public enum PunchType
{
    In,
    Out,
    Lunch,
    Transfer
}
