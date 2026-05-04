using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.Container;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class ContainerService : IContainerService
{
    private readonly ApplicationDbContext _dbContext;

    public static ContainerDto MapToDto(Container container) => new()
    {
        ContainerId = container.ContainerId,
        ContainerNumber = container.ContainerNumber,
        ContainerSize = container.ContainerSize,
        ContainerType = container.ContainerType,
        VGM = container.VGM,
        TrailerType = container.TrailerType,
        ConsigneeId = container.ConsigneeId,
        Consignee = container.ConsigneeCompany,
        ConsigneeName = container.ConsigneeCompany?.CompanyName ?? "Unknown",
        HaulierId = container.HaulierId,
        Haulier = container.HaulierCompany,
        HaulierName = container.HaulierCompany?.CompanyName ?? "Unknown",
        DepotId = container.DepotId,
        Depot = container.DepotCompany,
        DepotName = container.DepotCompany?.CompanyName ?? "Unknown",
        PortId = container.PortId,
        Port = container.PortCompany,
        PortName = container.PortCompany?.CompanyName ?? "Unknown",
        ROTDate = container.ROTDate,
        ToAddress = container.ToAddress?.Select(a => new ContainerAddressDto 
        { 
            Address = a.Address 
        }).ToList() ?? new List<ContainerAddressDto>(),
        ROTNumber = container.ROTNumber,
        Booking = container.Booking != null ? BookingService.MapToDto(container.Booking) : null,
        Status = container.Status,
        AssignedTime = container.AssignedTime,
        EnrouteTime = container.EnrouteTime,
        GatedInTime = container.GatedInTime,
        GatedOutTime = container.GatedOutTime,
        DeliveredTime =  container.DeliveredTime,
        RFCTime =   container.RFCTime,
        RejectedTime =  container.RejectedTime,
        DeletedTime =  container.DeletedTime,
        RTAssignedTime =   container.RTAssignedTime,
        RTEnrouteTime =   container.RTEnrouteTime,
        RTGatedInTime =    container.RTGatedInTime,
        RTGatedOutTime =   container.RTGatedOutTime,
        RTDeliveredTime =    container.RTDeliveredTime,
        RTRFCTime =    container.RTRFCTime,
        TimeStatus = container.TimeStatus,
        TurnAroundTime = container.TurnAroundTime,
        DGCRate = container.DGCRate,
        DGCReductionEligibility =  container.DGCReductionEligibility,
        DGCReduction =  container.DGCReduction,
        DeletedRemarks = container.DeletedRemarks,
    };
    
    public ContainerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ContainerDto>> GetAllAsync()
    {
        var containers = await _dbContext.Containers
            .Include(c => c.ToAddress)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.DepotCompany)
            .Include(c => c.PortCompany)
            .Include(c => c.Booking)
                .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.Booking)
                .ThenInclude(b => b.ShippingAgentCompany)
            .ToListAsync();
        return containers.Select(MapToDto);
    }

    public async Task<ContainerDto?> GetByIdAsync(int id)
    {
        var container = await _dbContext.Containers
            .Include(c => c.ToAddress)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.DepotCompany)
            .Include(c => c.PortCompany)
            .Include(c => c.Booking)
                .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.Booking)
                .ThenInclude(b => b.ShippingAgentCompany)
            .FirstOrDefaultAsync(c => c.ContainerId == id);
        return container == null? null : MapToDto(container);
    }

    public async Task<ContainerDto> CreateAsync(ContainerCreateDto dto)
    {
        var existing = await _dbContext.Containers.FirstOrDefaultAsync(c => c.ContainerNumber == dto.ContainerNumber && c.ROTNumber == dto.ROTNumber);
        if (existing != null)
            throw new Exception("Container ID already exists.");

        var container = new Models.Container
        {
            ContainerNumber = dto.ContainerNumber,
            ContainerSize = dto.ContainerSize,
            ContainerType = dto.ContainerType,
            VGM = dto.VGM,
            TrailerType = dto.TrailerType,
            ConsigneeId = dto.ConsigneeId,
            HaulierId = dto.HaulierId,
            DepotId = dto.DepotId,
            PortId = dto.PortId,
            ROTDate = dto.ROTDate,
            ToAddress = dto.ToAddress.Select(a => new ContainerAddress 
            { 
                Address = a.Address 
            }).ToList(),
            ROTNumber = dto.ROTNumber,
            Status = "Assigned",
            AssignedTime = dto.AssignedTime,
        };
        await _dbContext.Containers.AddAsync(container);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(container.ContainerId) ??  MapToDto(container);
    }

    public async Task<ContainerDto?> UpdateAsync(int id, ContainerUpdateDto dto)
    {
        var container = await _dbContext.Containers
            .Include(c => c.ToAddress)
            .FirstOrDefaultAsync(c => c.ContainerId == id);
        if (container == null)
        {
            return null;
        }

        container.ContainerNumber = dto.ContainerNumber;
        container.ContainerSize = dto.ContainerSize;
        container.ContainerType = dto.ContainerType;
        container.VGM = dto.VGM;
        container.TrailerType = dto.TrailerType;
        container.ConsigneeId = dto.ConsigneeId;
        container.HaulierId = dto.HaulierId;
        container.DepotId = dto.DepotId;
        container.PortId = dto.PortId;
        container.ROTDate = dto.ROTDate;
        _dbContext.ContainerAddresses.RemoveRange(container.ToAddress);
        foreach (var addrDto in dto.ToAddress)
        {
            container.ToAddress.Add(new ContainerAddress { Address = addrDto.Address });
        }
        container.ROTNumber = dto.ROTNumber;
        container.Status = dto.Status;
        container.AssignedTime = dto.AssignedTime;
        container.EnrouteTime = dto.EnrouteTime;
        container.GatedInTime = dto.GatedInTime;
        container.GatedOutTime = dto.GatedOutTime;
        container.DeliveredTime = dto.DeliveredTime;
        container.RFCTime = dto.RFCTime;
        container.RejectedTime = dto.RejectedTime;
        container.DeletedTime = dto.DeletedTime;
        container.RTAssignedTime = dto.RTAssignedTime;
        container.RTEnrouteTime = dto.RTEnrouteTime;
        container.RTGatedInTime = dto.RTGatedInTime;
        container.RTGatedOutTime = dto.RTGatedOutTime;
        container.RTDeliveredTime = dto.RTDeliveredTime;
        container.RTRFCTime = dto.RTRFCTime;
        container.DeletedRemarks = dto.DeletedRemarks;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(container.ContainerId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var container = await _dbContext.Containers
            .FirstOrDefaultAsync(c => c.ContainerId == id);
        if (container == null)
            return false;

        // Remove the Address records of containers
        var addresses = await _dbContext.ContainerAddresses
            .Where(a => a.ContainerId == id).ToListAsync();
        if (addresses.Any())
        {
            _dbContext.ContainerAddresses.RemoveRange(addresses);
        }

        // Remove the Assigned Haulier records
        var assignments = await _dbContext.AssignedHauliers
            .Where(a => a.ContainerId == id).ToListAsync();
        if (assignments.Any())
        {
            _dbContext.AssignedHauliers.RemoveRange(assignments);
        }

        _dbContext.Containers.Remove(container);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ContainerDto>> GetAllContainersByForwarding(string forwarderId)
    {
        var containers = await _dbContext.Containers
            .Include(c => c.ToAddress)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.DepotCompany)
            .Include(c => c.PortCompany)
            .Include(c => c.Booking)
                .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.Booking)
                .ThenInclude(b => b.ShippingAgentCompany)
            .Where(c => c.Booking.ForwardingId == forwarderId)
            .ToListAsync();
        return containers.Select(MapToDto);
    }
    
    public async Task<IEnumerable<ContainerDto>> GetAllContainersByHaulier(string haulierId)
    {
        var containers = await _dbContext.Containers
            .Include(c => c.ToAddress)
            .Include(c => c.ConsigneeCompany)  
            .Include(c => c.HaulierCompany)
            .Include(c => c.DepotCompany)
            .Include(c => c.PortCompany)
            .Include(c => c.Booking)
            .ThenInclude(b => b.ForwardingCompany) 
            .Include(c => c.Booking)
            .ThenInclude(b => b.ShippingAgentCompany)
            .Where(c => c.HaulierId == haulierId)
            .ToListAsync();
        return containers.Select(MapToDto);
    }
}