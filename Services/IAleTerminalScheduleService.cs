using CLE_BackEnd.DTOs.AleTerminalSchedule;

namespace CLE_BackEnd.Services;

public interface IAleTerminalScheduleService
{
    Task<AleTerminalScheduleDto?> GetByTerminalIdAsync(string terminalId);
    Task<AleTerminalScheduleDto> SaveTemplateAsync(AleTerminalScheduleCreateDto dto);
}