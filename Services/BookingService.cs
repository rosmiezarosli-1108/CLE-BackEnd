using System.Collections;
using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.Booking;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _dbContext;

    public static BookingDto MapToDto(Booking booking) => new()
    {
        ROTNumber =  booking.ROTNumber,
        BLOrBookingNumber = booking.BLOrBookingNumber,
        HouseBLNumber =  booking.HouseBLNumber,
        MovementType = booking.MovementType,
        TripType = booking.TripType,
        SCN = booking.SCN,
        PortLocation = booking.PortLocation,
        Port = booking.PortLocationCompany,
        PortLocationName = booking.PortLocationCompany?.CompanyName ?? "Unknown",
        ETA = booking.ETA ?? new DateOnly(),
        SealNumber = booking.SealNumber,
        ForwardingRemarks = booking.ForwardingRemarks,
        HaulierRemarks = booking.HaulierRemarks,
        DepotRemarks = booking.DepotRemarks,
        ForwardingId = booking.ForwardingId,
        Forwarding = booking.ForwardingCompany,
        ForwardingName = booking.ForwardingCompany?.CompanyName ?? "Unknown",
        ShippingAgentId = booking.ShippingAgentId,
        ShippingAgent = booking.ShippingAgentCompany,
        ShippingAgentName = booking.ShippingAgentCompany?.CompanyName ?? "Unknown",
        BillingParty = booking.BillingParty,
        CustomFormNo = booking.CustomFormNo,
        CustomReceiptNo =  booking.CustomReceiptNo,
        DICNumber =  booking.DICNumber,
        ZBNumber =  booking.ZBNumber,
        ContainerQuantity =  booking.ContainerQuantity,
    };
    
    public BookingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookingDto>> GetAllAsync()
    {
        var bookings = await _dbContext.Bookings
            .Include(b => b.ForwardingCompany)       
            .Include(b => b.ShippingAgentCompany)   
            .ToListAsync();
        return bookings.Select(MapToDto);
    }

    public async Task<BookingDto?> GetByIdAsync(string id)
    {
        var booking = await _dbContext.Bookings
            .Include(b => b.ForwardingCompany)
            .Include(b => b.ShippingAgentCompany)
            
            .FirstOrDefaultAsync(b => b.ROTNumber == id);
        return booking == null? null : MapToDto(booking);
    }

    public async Task<BookingDto> CreateAsync(BookingCreateDto dto)
    {
        var existing = await _dbContext.Bookings.AnyAsync(b => b.ROTNumber == dto.ROTNumber);
        if (existing) throw new Exception("ROT Number already exists.");
        
        var booking = new Models.Booking
        {
            ROTNumber =  dto.ROTNumber,
            BLOrBookingNumber = dto.BLOrBookingNumber,
            HouseBLNumber =  dto.HouseBLNumber,
            MovementType = dto.MovementType,
            TripType = dto.TripType,
            SCN =  dto.SCN,
            PortLocation = dto.PortLocation,
            ETA = dto.ETA,
            SealNumber = dto.SealNumber,
            ForwardingRemarks = dto.ForwardingRemarks,
            HaulierRemarks = dto.HaulierRemarks,
            DepotRemarks = dto.DepotRemarks,
            ForwardingId = dto.ForwardingId,
            ShippingAgentId = dto.ShippingAgentId,
            BillingParty = dto.BillingParty,
            CustomFormNo = dto.CustomFormNo,
            CustomReceiptNo = dto.CustomReceiptNo,
            DICNumber = dto.DICNumber,
            ZBNumber = dto.ZBNumber,
            ContainerQuantity = dto.ContainerQuantity,
        };
        await _dbContext.Bookings.AddAsync(booking);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(booking.BLOrBookingNumber) ??  MapToDto(booking);
    }

    public async Task<BookingDto?> UpdateAsync(string id, BookingUpdateDto dto)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.ROTNumber == id);
        if (booking == null)
        {
            return null;
        }

        booking.BLOrBookingNumber = dto.BLOrBookingNumber;
        booking.HouseBLNumber = dto.HouseBLNumber;
        booking.MovementType = dto.MovementType;
        booking.TripType = dto.TripType;
        booking.SCN =  dto.SCN;
        booking.PortLocation = dto.PortLocation;
        booking.ETA = dto.ETA;
        booking.SealNumber = dto.SealNumber;
        booking.ForwardingRemarks = dto.ForwardingRemarks;
        booking.HaulierRemarks = dto.HaulierRemarks;
        booking.DepotRemarks = dto.DepotRemarks;
        booking.ForwardingId = dto.ForwardingId;
        booking.ShippingAgentId = dto.ShippingAgentId;
        booking.BillingParty = dto.BillingParty;
        booking.CustomFormNo = dto.CustomFormNo;
        booking.CustomReceiptNo = dto.CustomReceiptNo;
        booking.DICNumber = dto.DICNumber;
        booking.ZBNumber = dto.ZBNumber;
        booking.ContainerQuantity = dto.ContainerQuantity;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(booking.BLOrBookingNumber);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var booking = await _dbContext.Bookings
            .FirstOrDefaultAsync(b => b.ROTNumber == id);
        if (booking == null)
            return false;

        //to remove the containers of the booking
        var bookingContainers = await _dbContext.Containers
            .Where(c => c.ROTNumber == id).ToListAsync();
        
        //to remove the documents of the booking
        var bookingDocuments = await _dbContext.BookingDocuments
            .Where(c => c.ROTNumber == id).ToListAsync();
        
        //to remove assignedHauliers and addressees
        if (bookingContainers.Any())
        {
            var containerIds = bookingContainers.Select(c => c.ContainerId).ToList();

            var addresses = await _dbContext.ContainerAddresses
                .Where(a => containerIds.Contains(a.ContainerId))
                .ToListAsync();
            if (addresses.Any()) _dbContext.ContainerAddresses.RemoveRange(addresses);

            var assignedHauliers = await _dbContext.AssignedHauliers
                .Where(a => a.ROTNumber == id)
                .ToListAsync();
            if (assignedHauliers.Any()) _dbContext.AssignedHauliers.RemoveRange(assignedHauliers);

            _dbContext.Containers.RemoveRange(bookingContainers);
        }

        _dbContext.Bookings.Remove(booking);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookingsByForwarding(string forwarderId)
    {
        var bookings = await _dbContext.Bookings
            .Include(b => b.ForwardingCompany)       
            .Include(b => b.ShippingAgentCompany)
            .Where(b => b.ForwardingId == forwarderId)
            .ToListAsync();
        return bookings.Select(MapToDto);
    }
    
    public async Task<IEnumerable<BookingDto>> GetAllBookingsByHaulier(string haulierId)
    {
        var bookings = await _dbContext.Bookings
            .Include(b => b.ForwardingCompany)       
            .Include(b => b.ShippingAgentCompany)
            .Where(b => b.ForwardingId == haulierId)
            .ToListAsync();
        return bookings.Select(MapToDto);
    }
}
