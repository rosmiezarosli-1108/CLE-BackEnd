using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.AleTerminalSchedule;

public class AleTerminalScheduleDto
{
    public string TerminalId { get; set; } = string.Empty;
    public int MaximumPickUpSlots { get; set; }
    public int MaximumDropOffSlots { get; set; }
    public int AutoAcceptMinutes { get; set; }
    public int AutoRejectMinutes { get; set; }

    public string? MonStart { get; set; }
    public string? MonEnd { get; set; }
    public string? MonBreakStart { get; set; }
    public string? MonBreakEnd { get; set; }

    public string? TueStart { get; set; }
    public string? TueEnd { get; set; }
    public string? TueBreakStart { get; set; }
    public string? TueBreakEnd { get; set; }

    public string? WedStart { get; set; }
    public string? WedEnd { get; set; }
    public string? WedBreakStart { get; set; }
    public string? WedBreakEnd { get; set; }

    public string? ThuStart { get; set; }
    public string? ThuEnd { get; set; }
    public string? ThuBreakStart { get; set; }
    public string? ThuBreakEnd { get; set; }

    public string? FriStart { get; set; }
    public string? FriEnd { get; set; }
    public string? FriBreakStart { get; set; }
    public string? FriBreakEnd { get; set; }

    public string? SatStart { get; set; }
    public string? SatEnd { get; set; }
    public string? SatBreakStart { get; set; }
    public string? SatBreakEnd { get; set; }

    public string? SunStart { get; set; }
    public string? SunEnd { get; set; }
    public string? SunBreakStart { get; set; }
    public string? SunBreakEnd { get; set; }
}

public class AleTerminalScheduleCreateDto
{
    [Required]
    public string TerminalId { get; set; } = string.Empty;
    public int MaximumPickUpSlots { get; set; }
    public int MaximumDropOffSlots { get; set; }
    public int AutoAcceptMinutes { get; set; }
    public int AutoRejectMinutes { get; set; }

    public string? MonStart { get; set; }
    public string? MonEnd { get; set; }
    public string? MonBreakStart { get; set; }
    public string? MonBreakEnd { get; set; }

    public string? TueStart { get; set; }
    public string? TueEnd { get; set; }
    public string? TueBreakStart { get; set; }
    public string? TueBreakEnd { get; set; }

    public string? WedStart { get; set; }
    public string? WedEnd { get; set; }
    public string? WedBreakStart { get; set; }
    public string? WedBreakEnd { get; set; }

    public string? ThuStart { get; set; }
    public string? ThuEnd { get; set; }
    public string? ThuBreakStart { get; set; }
    public string? ThuBreakEnd { get; set; }

    public string? FriStart { get; set; }
    public string? FriEnd { get; set; }
    public string? FriBreakStart { get; set; }
    public string? FriBreakEnd { get; set; }

    public string? SatStart { get; set; }
    public string? SatEnd { get; set; }
    public string? SatBreakStart { get; set; }
    public string? SatBreakEnd { get; set; }

    public string? SunStart { get; set; }
    public string? SunEnd { get; set; }
    public string? SunBreakStart { get; set; }
    public string? SunBreakEnd { get; set; }
}