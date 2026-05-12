using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleContainer;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AleContainerService : IAleContainerService
{
    private readonly ApplicationDbContext _dbContext;
    
    public static AleContainerDto MapToDto(AleContainer aleContainer) => new()
    {
        ContainerId = aleContainer.ContainerId,
        ContainerNumber = aleContainer.ContainerNumber,
        ContainerSize = aleContainer.ContainerSize,
        ContainerType = aleContainer.ContainerType,
        VGM = aleContainer.VGM,
        TrailerType = aleContainer.TrailerType,
        ConsigneeId = aleContainer.ConsigneeId,
        Consignee = aleContainer.ConsigneeCompany,
        ConsigneeName = aleContainer.ConsigneeCompany?.CompanyName ?? "Unknown",
        HaulierId = aleContainer.HaulierId,
        Haulier = aleContainer.HaulierCompany,
        HaulierName = aleContainer.HaulierCompany?.CompanyName ?? "Unknown",
        TerminalId = aleContainer.TerminalId,
        Terminal = aleContainer.TerminalCompany,
        TerminalName = aleContainer.TerminalCompany?.CompanyName ?? "Unknown",
        ROTDate = aleContainer.ROTDate,
        ToAddress = aleContainer.ToAddress?.Select(a => new AleContainerAddressDto 
        { 
            Address = a.Address 
        }).ToList() ?? new List<AleContainerAddressDto>(),
        ROTNumber = aleContainer.ROTNumber,
        AleBooking = aleContainer.AleBooking != null ? AleBookingService.MapToDto(aleContainer.AleBooking) : null,
        Status = aleContainer.Status,
        AssignedTime = aleContainer.AssignedTime,
        EnrouteTime = aleContainer.EnrouteTime,
        AcceptedTime = aleContainer.AcceptedTime,
        GatedInTime = aleContainer.GatedInTime,
        GatedOutTime = aleContainer.GatedOutTime,
        DeliveredTime = aleContainer.DeliveredTime,
        RFCTime = aleContainer.RFCTime,
        RejectedTime =  aleContainer.RejectedTime,
        DeletedTime =  aleContainer.DeletedTime,
        RTAssignedTime = aleContainer.RTAssignedTime,
        RTEnrouteTime = aleContainer.RTEnrouteTime,
        RTAcceptedTime = aleContainer.RTAcceptedTime,
        RTGatedInTime = aleContainer.RTGatedInTime,
        RTGatedOutTime = aleContainer.RTGatedOutTime,
        RTDeliveredTime = aleContainer.RTDeliveredTime,
        RTRFCTime = aleContainer.RTRFCTime,
        TimeStatus = aleContainer.TimeStatus,
        TurnAroundTime = aleContainer.TurnAroundTime,
        DGCRate = aleContainer.DGCRate,
        DGCReductionEligibility =  aleContainer.DGCReductionEligibility,
        DGCReduction =  aleContainer.DGCReduction,
        DeletedRemarks = aleContainer.DeletedRemarks,
        RejectedRemarks = aleContainer.RejectedRemarks,
        UpdateHistory = aleContainer.UpdateHistory?.Select(h => new AleContainerAuditsDto
        {
            UpdatedBy = h.UpdatedBy,
            UpdatedTime = h.UpdatedTime,
            Action = h.Action,
        }).OrderByDescending(h => h.UpdatedTime).ToList() ?? new List<AleContainerAuditsDto>(),
        ReceivedBy = aleContainer.ReceivedBy,
        ApprovedCustomsTime = aleContainer.ApprovedCustomsTime,
        ApprovedAKPSTime = aleContainer.ApprovedAKPSTime,
        ApprovedBothTime = aleContainer.ApprovedBothTime,
        ExamineBothTime = aleContainer.ExamineBothTime,
        ExamineAKPSTime = aleContainer.ExamineAKPSTime, 
        ExamineCustomTime = aleContainer.ExamineAKPSTime, 
        RejectedAKPSTime = aleContainer.RejectedAKPSTime,
        RejectedCustomTime = aleContainer.RejectedCustomTime,
        RejectedBothTime = aleContainer.RejectedBothTime,
        EditRemarks =  aleContainer.EditRemarks,
        PackageQuantity =  aleContainer.PackageQuantity,
        VolumeMetricWeight = aleContainer.VolumeMetricWeight,
    };
    
    public AleContainerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleContainerDto>> GetAllAsync()
    {
        var aleContainers = await _dbContext.AleContainers
            .Include(c => c.ToAddress)
            .Include(c => c.UpdateHistory)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.TerminalCompany)
            .Include(c => c.AleBooking)
                .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.AleBooking)
                .ThenInclude(b => b.AirlineCompany)
            .ToListAsync();
        return aleContainers.Select(MapToDto);
    }

    public async Task<AleContainerDto?> GetByIdAsync(int id)
    {
        var aleContainer = await _dbContext.AleContainers
            .Include(c => c.ToAddress)
            .Include(c => c.UpdateHistory)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.TerminalCompany)
            .Include(c => c.AleBooking)
                .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.AleBooking)
                .ThenInclude(b => b.AirlineCompany)
            .FirstOrDefaultAsync(c => c.ContainerId == id);
        return aleContainer == null? null : MapToDto(aleContainer);
    }

    public async Task<AleContainerDto> CreateAsync(AleContainerCreateDto dto)
    {
        var existing = await _dbContext.AleContainers.FirstOrDefaultAsync(c => c.ContainerNumber == dto.ContainerNumber && c.ROTNumber == dto.ROTNumber);
        if (existing != null)
            throw new Exception("Container ID already exists.");

        var aleContainer = new Models.AleContainer
        {
            ContainerNumber = dto.ContainerNumber,
            ContainerSize = dto.ContainerSize,
            ContainerType = dto.ContainerType,
            VGM = dto.VGM,
            TrailerType = dto.TrailerType,
            ConsigneeId = dto.ConsigneeId,
            HaulierId = dto.HaulierId,
            TerminalId = dto.TerminalId,
            ROTDate = dto.ROTDate,
            ToAddress = dto.ToAddress.Select(a => new AleContainerAddress 
            { 
                Address = a.Address 
            }).ToList(),
            ROTNumber = dto.ROTNumber,
            Status = dto.Status,
            AssignedTime = dto.AssignedTime,
            EnrouteTime = dto.EnrouteTime,
            PackageQuantity = dto.PackageQuantity,
            VolumeMetricWeight = dto.VolumeMetricWeight,
        };
        await _dbContext.AleContainers.AddAsync(aleContainer);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(aleContainer.ContainerId) ??  MapToDto(aleContainer);
    }

    public async Task<AleContainerDto?> UpdateAsync(int id, AleContainerUpdateDto dto, string updatedBy)
    {
        var aleContainer = await _dbContext.AleContainers
            .Include(c => c.ToAddress)
            .Include(c => c.UpdateHistory)
            .FirstOrDefaultAsync(c => c.ContainerId == id);
        if (aleContainer == null)
        {
            return null;
        }

        aleContainer.ContainerNumber = dto.ContainerNumber;
        aleContainer.ContainerSize = dto.ContainerSize;
        aleContainer.ContainerType = dto.ContainerType;
        aleContainer.VGM = dto.VGM;
        aleContainer.TrailerType = dto.TrailerType;
        aleContainer.ConsigneeId = dto.ConsigneeId;
        aleContainer.HaulierId = dto.HaulierId;
        aleContainer.TerminalId = dto.TerminalId;
        aleContainer.ROTDate = dto.ROTDate;
        _dbContext.AleContainerAddresses.RemoveRange(aleContainer.ToAddress);
        foreach (var addrDto in dto.ToAddress)
        {
            aleContainer.ToAddress.Add(new AleContainerAddress { Address = addrDto.Address });
        }
        aleContainer.ROTNumber = dto.ROTNumber;
        aleContainer.Status = dto.Status;
        aleContainer.AssignedTime = dto.AssignedTime;
        aleContainer.EnrouteTime = dto.EnrouteTime;
        aleContainer.AcceptedTime = dto.AcceptedTime;
        aleContainer.GatedInTime = dto.GatedInTime;
        aleContainer.GatedOutTime = dto.GatedOutTime;
        aleContainer.DeliveredTime = dto.DeliveredTime;
        aleContainer.RFCTime = dto.RFCTime;
        aleContainer.RejectedTime = dto.RejectedTime;
        aleContainer.DeletedTime = dto.DeletedTime;
        aleContainer.RTAssignedTime = dto.RTAssignedTime;
        aleContainer.RTEnrouteTime = dto.RTEnrouteTime;
        aleContainer.RTAcceptedTime = dto.RTAcceptedTime;
        aleContainer.RTGatedInTime = dto.RTGatedInTime;
        aleContainer.RTGatedOutTime = dto.RTGatedOutTime;
        aleContainer.RTDeliveredTime = dto.RTDeliveredTime;
        aleContainer.RTRFCTime = dto.RTRFCTime;
        aleContainer.DeletedRemarks = dto.DeletedRemarks;
        aleContainer.RejectedRemarks = dto.RejectedRemarks;
        aleContainer.ReceivedBy = dto.ReceivedBy;
        aleContainer.UpdateHistory.Add(new AleContainerAudit
        {
            UpdatedBy = updatedBy,
            UpdatedTime = DateTime.UtcNow, 
            Action = $"Container updated. Status: {dto.Status}"
        });
        aleContainer.ApprovedAKPSTime = dto.ApprovedAKPSTime;
        aleContainer.ApprovedCustomsTime = dto.ApprovedCustomsTime;
        aleContainer.ApprovedBothTime = dto.ApprovedBothTime;
        aleContainer.ExamineBothTime = dto.ExamineBothTime;
        aleContainer.ExamineAKPSTime = dto.ExamineAKPSTime;
        aleContainer.ExamineCustomTime = dto.ExamineAKPSTime;
        aleContainer.RejectedAKPSTime = dto.RejectedAKPSTime;
        aleContainer.RejectedCustomTime = dto.RejectedCustomTime;
        aleContainer.RejectedBothTime = dto.RejectedBothTime;
        aleContainer.EditRemarks = dto.EditRemarks;
        aleContainer.PackageQuantity = dto.PackageQuantity;
        aleContainer.VolumeMetricWeight = dto.VolumeMetricWeight;
        
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(aleContainer.ContainerId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var aleContainer = await _dbContext.AleContainers
            .FirstOrDefaultAsync(c => c.ContainerId == id);
        if (aleContainer == null)
            return false;

        // Remove the Audit records of aleContainers
        var audits = await _dbContext.AleContainerAudits
            .Where(a => a.ContainerId == id).ToListAsync();
        if (audits.Any())
        {
            _dbContext.AleContainerAudits.RemoveRange(audits);
        }
        
        // Remove the Address records of aleContainers
        var addresses = await _dbContext.AleContainerAddresses
            .Where(a => a.ContainerId == id).ToListAsync();
        if (addresses.Any())
        {
            _dbContext.AleContainerAddresses.RemoveRange(addresses);
        }

        // Remove the Assigned Haulier records
        var assignments = await _dbContext.AssignedHauliers
            .Where(a => a.ContainerId == id).ToListAsync();
        if (assignments.Any())
        {
            _dbContext.AssignedHauliers.RemoveRange(assignments);
        }

        _dbContext.AleContainers.Remove(aleContainer);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<AleContainerDto>> GetAllAleContainersByForwarding(string forwarderId)
    {
        var aleContainers = await _dbContext.AleContainers
            .Include(c => c.ToAddress)
            .Include(c => c.UpdateHistory)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.TerminalCompany)
            .Include(c => c.AleBooking)
                .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.AleBooking)
                .ThenInclude(b => b.AirlineCompany)
            .Where(c => c.AleBooking.ForwardingId == forwarderId)
            .ToListAsync();
        return aleContainers.Select(MapToDto);
    }
    
    public async Task<IEnumerable<AleContainerDto>> GetAllAleContainersByHaulier(string haulierId)
    {
        var aleContainers = await _dbContext.AleContainers
            .Include(c => c.ToAddress)
            .Include(c => c.UpdateHistory)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.TerminalCompany)
            .Include(c => c.AleBooking)
            .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.AleBooking)
            .ThenInclude(b => b.AirlineCompany)
            .Where(c => c.HaulierId == haulierId)
            .ToListAsync();
        return aleContainers.Select(MapToDto);
    }
}