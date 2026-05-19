using System.Collections;
using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs;
using CLE_BackEnd.DTOs.AleBooking;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AleBookingService : IAleBookingService
{
    private readonly ApplicationDbContext _dbContext;

    public static AleBookingDto MapToDto(AleBooking aleBooking) => new()
    {
        ROTNumber =  aleBooking.ROTNumber,
        AWBNumber = aleBooking.AWBNumber,
        HouseAWBNumber =  aleBooking.HouseAWBNumber,
        MovementType = aleBooking.MovementType,
        TripType = aleBooking.TripType,
        FlightNumber = aleBooking.FlightNumber,
        TerminalLocation = aleBooking.TerminalLocation,
        Terminal = aleBooking.TerminalCompany,
        TerminalLocationName = aleBooking.TerminalCompany?.CompanyName ?? "Unknown",
        ETA = aleBooking.ETA ?? new DateOnly(),
        SealNumber = aleBooking.SealNumber,
        ForwardingRemarks = aleBooking.ForwardingRemarks,
        HaulierRemarks = aleBooking.HaulierRemarks,
        TerminalRemarks = aleBooking.TerminalRemarks,
        ForwardingId = aleBooking.ForwardingId,
        Forwarding = aleBooking.ForwardingCompany,
        ForwardingName = aleBooking.ForwardingCompany?.CompanyName ?? "Unknown",
        AirlineId = aleBooking.AirlineId,
        Airline = aleBooking.AirlineCompany,
        AirlineName = aleBooking.AirlineCompany?.CompanyName ?? "Unknown",
        BillingParty = aleBooking.BillingParty,
        CustomFormType =  aleBooking.CustomFormType,
        CustomFormNo = aleBooking.CustomFormNo,
        CustomReceiptNo =  aleBooking.CustomReceiptNo,
        DICNumber =  aleBooking.DICNumber,
        ZBNumber =  aleBooking.ZBNumber,
        TruckQuantity = aleBooking.TruckQuantity,
        CarrierReferenceNumber = aleBooking.CarrierReferenceNumber,
        TotalPackageQuantity = aleBooking.TotalPackageQuantity,
        Weight =  aleBooking.Weight,
        ConsigneeId = aleBooking.ConsigneeId,
        ConsigneeCompany = aleBooking.ConsigneeCompany,
        SSMNumber = aleBooking.SSMNumber,
        ExternalConsigneeName =  aleBooking.ExternalConsigneeName,
        ExternalConsigneeAddress = aleBooking.ExternalConsigneeAddress,
        ExternalConsigneeContact =  aleBooking.ExternalConsigneeContact,
        Size = aleBooking.Size,
    };
    
    public AleBookingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleBookingDto>> GetAllAsync()
    {
        var aleBookings = await _dbContext.AleBookings
            .Include(b => b.ForwardingCompany)       
            .Include(b => b.AirlineCompany)   
            .ToListAsync();
        return aleBookings.Select(MapToDto);
    }

    public async Task<AleBookingDto?> GetByIdAsync(string id)
    {
        var aleBooking = await _dbContext.AleBookings
            .Include(b => b.ForwardingCompany)
            .Include(b => b.AirlineCompany)
            
            .FirstOrDefaultAsync(b => b.ROTNumber == id);
        return aleBooking == null? null : MapToDto(aleBooking);
    }

    public async Task<AleBookingDto> CreateAsync(AleBookingCreateDto dto)
    {
        var existing = await _dbContext.AleBookings.AnyAsync(b => b.ROTNumber == dto.ROTNumber);
        if (existing) throw new Exception("ROT Number already exists.");
        
        var aleBooking = new Models.AleBooking
        {
            ROTNumber =  dto.ROTNumber,
            AWBNumber = dto.AWBNumber,
            HouseAWBNumber =  dto.HouseAWBNumber,
            MovementType = dto.MovementType,
            TripType = dto.TripType,
            FlightNumber =  dto.FlightNumber,
            TerminalLocation = dto.TerminalLocation,
            ETA = dto.ETA,
            SealNumber = dto.SealNumber,
            ForwardingRemarks = dto.ForwardingRemarks,
            HaulierRemarks = dto.HaulierRemarks,
            TerminalRemarks = dto.TerminalRemarks,
            ForwardingId = dto.ForwardingId,
            AirlineId = dto.AirlineId,
            BillingParty = dto.BillingParty,
            CustomFormType = dto.CustomFormType,
            CustomFormNo = dto.CustomFormNo,
            CustomReceiptNo = dto.CustomReceiptNo,
            DICNumber = dto.DICNumber,
            ZBNumber = dto.ZBNumber,
            TruckQuantity = dto.TruckQuantity,
            CarrierReferenceNumber =  dto.CarrierReferenceNumber,
            TotalPackageQuantity =  dto.TotalPackageQuantity,
            Weight =  dto.Weight,
            ConsigneeId = dto.ConsigneeId,
            SSMNumber = dto.SSMNumber,
            ExternalConsigneeName = dto.ExternalConsigneeName,
            ExternalConsigneeAddress = dto.ExternalConsigneeAddress,
            ExternalConsigneeContact = dto.ExternalConsigneeContact,
            Size = dto.Size,
        };
        await _dbContext.AleBookings.AddAsync(aleBooking);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(aleBooking.ROTNumber) ??  MapToDto(aleBooking);
    }

    public async Task<AleBookingDto?> UpdateAsync(string id, AleBookingUpdateDto dto)
    {
        var aleBooking = await _dbContext.AleBookings.FirstOrDefaultAsync(b => b.ROTNumber == id);
        if (aleBooking == null)
        {
            return null;
        }

        aleBooking.AWBNumber = dto.AWBNumber;
        aleBooking.HouseAWBNumber = dto.HouseAWBNumber;
        aleBooking.MovementType = dto.MovementType;
        aleBooking.TripType = dto.TripType;
        aleBooking.FlightNumber = dto.FlightNumber;
        aleBooking.TerminalLocation = dto.TerminalLocation;
        aleBooking.ETA = dto.ETA;
        aleBooking.SealNumber = dto.SealNumber;
        aleBooking.ForwardingRemarks = dto.ForwardingRemarks;
        aleBooking.HaulierRemarks = dto.HaulierRemarks;
        aleBooking.TerminalRemarks = dto.TerminalRemarks;
        aleBooking.ForwardingId = dto.ForwardingId;
        aleBooking.AirlineId = dto.AirlineId;
        aleBooking.BillingParty = dto.BillingParty;
        aleBooking.CustomFormType = dto.CustomFormType;
        aleBooking.CustomFormNo = dto.CustomFormNo;
        aleBooking.CustomReceiptNo = dto.CustomReceiptNo;
        aleBooking.DICNumber = dto.DICNumber;
        aleBooking.ZBNumber = dto.ZBNumber;
        aleBooking.TruckQuantity = dto.TruckQuantity;
        aleBooking.CarrierReferenceNumber = dto.CarrierReferenceNumber;
        aleBooking.TotalPackageQuantity = dto.TotalPackageQuantity;
        aleBooking.Weight = dto.Weight;
        aleBooking.ConsigneeId = dto.ConsigneeId;
        aleBooking.SSMNumber = dto.SSMNumber;
        aleBooking.ExternalConsigneeName = dto.ExternalConsigneeName;
        aleBooking.ExternalConsigneeAddress = dto.ExternalConsigneeAddress;
        aleBooking.ExternalConsigneeContact = dto.ExternalConsigneeContact;
        aleBooking.Size = dto.Size;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(aleBooking.ROTNumber);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var aleBooking = await _dbContext.AleBookings
            .FirstOrDefaultAsync(b => b.ROTNumber == id);
        if (aleBooking == null)
            return false;

        //to remove the containers of the aleBooking
        var aleBookingContainers = await _dbContext.Containers
            .Where(c => c.ROTNumber == id).ToListAsync();
        
        //to remove the documents of the aleBooking
        var aleBookingDocuments = await _dbContext.BookingDocuments
            .Where(c => c.ROTNumber == id).ToListAsync();
        
        //to remove assignedHauliers and addressees
        if (aleBookingContainers.Any())
        {
            var containerIds = aleBookingContainers.Select(c => c.ContainerId).ToList();

            var addresses = await _dbContext.ContainerAddresses
                .Where(a => containerIds.Contains(a.ContainerId))
                .ToListAsync();
            if (addresses.Any()) _dbContext.ContainerAddresses.RemoveRange(addresses);

            var assignedHauliers = await _dbContext.AssignedHauliers
                .Where(a => a.ROTNumber == id)
                .ToListAsync();
            if (assignedHauliers.Any()) _dbContext.AssignedHauliers.RemoveRange(assignedHauliers);

            _dbContext.Containers.RemoveRange(aleBookingContainers);
        }

        _dbContext.AleBookings.Remove(aleBooking);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<AleBookingDto>> GetAllAleBookingsByForwarding(string forwarderId)
    {
        var aleBookings = await _dbContext.AleBookings
            .Include(b => b.ForwardingCompany)       
            .Include(b => b.AirlineCompany)
            .Where(b => b.ForwardingId == forwarderId)
            .ToListAsync();
        return aleBookings.Select(MapToDto);
    }
    
    public async Task<IEnumerable<AleBookingDto>> GetAllAleBookingsByHaulier(string haulierId)
    {
        var aleBookings = await _dbContext.AleBookings
            .Include(b => b.ForwardingCompany)       
            .Include(b => b.AirlineCompany)
            .Where(b => b.ForwardingId == haulierId)
            .ToListAsync();
        return aleBookings.Select(MapToDto);
    }
}
