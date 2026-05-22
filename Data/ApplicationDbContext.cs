using Microsoft.EntityFrameworkCore;
using CLE_BackEnd.Models;

namespace CLE_BackEnd.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<ContainerAddress> ContainerAddresses { get; set; }
    public DbSet<AssignedHaulier> AssignedHauliers { get; set; }
    public DbSet<BookingDocument> BookingDocuments { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<PrimeMover> PrimeMovers { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<ContainerAudit> ContainerAudits { get; set; }
    public DbSet<AleContainerAudit> AleContainerAudits { get; set; }
    public DbSet<AleContainer> AleContainers { get; set; }
    public DbSet<AleContainerAddress> AleContainerAddresses { get; set; }
    public DbSet<AleBooking> AleBookings { get; set; }
    public DbSet<AleBookingDocument> AleBookingDocuments { get; set; }
    public DbSet<AleAssignedHaulier> AleAssignedHauliers { get; set; }
    public DbSet<AleTimeSlot> AleTimeSlots { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var foreignKeys = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())
            .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in foreignKeys)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        modelBuilder.Entity<AleContainerAudit>()
            .HasOne<AleContainer>()
            .WithMany(c => c.UpdateHistory)
            .HasForeignKey(a => a.ContainerId); // Force it to use ContainerId instead of AleContainerContainerId
        
        //modelBuilder.Entity<Company>(entity =>
        //{
            //entity.OwnsMany(c => c.Region, r =>
            //{
               // r.ToJson(); 
            //});
        //});
        
        // modelBuilder.Entity<Company>().HasData(
        //     new Company
        //     {
        //         CompanyCode = "A0001",
        //         CompanyName = "ABC Forwarders",
        //         SSMNo = "123456-A",
        //         SSTNo = "W10-1234-5678",
        //         Role = "Forwarder",
        //         Region = new List<SystemRegion>
        //         {
        //             new SystemRegion 
        //             { 
        //                 SystemName = "CLE", 
        //                 RegionCode = "PNG" 
        //             },
        //             new SystemRegion 
        //             { 
        //                 SystemName = "ALE", 
        //                 RegionCode = "PEN" 
        //             }
        //         },
        //         ManagerName = "Pradeep",
        //         Address = "Butterworth, 13000, Penang",
        //         TelephoneNumber = "03-12345678",
        //         FaxNumber = "03-12345679",
        //         PICName = "Thanesh",
        //         HandphoneNumber = "012-3456789",
        //         EmailAddress = "nesh@gmail.com.my",
        //         CCEmailAddress = "finance@gmail.com.my",
        //         CLEKmailNotification = "operater@gmail.com"
        //     },
        //     new Company
        //     {
        //         CompanyCode = "A0002",
        //         CompanyName = "ABC Haulier",
        //         SSMNo = "123456-B",
        //         SSTNo = "W11-1234-5678",
        //         Role = "Haulier",
        //         Region = new List<SystemRegion>
        //         {
        //             new SystemRegion 
        //             { 
        //                 SystemName = "CLE", 
        //                 RegionCode = "PNG" 
        //             },
        //             new SystemRegion 
        //             { 
        //                 SystemName = "ALE", 
        //                 RegionCode = "PEN" 
        //             }
        //         },
        //         ManagerName = "Tristen",
        //         Address = "Port Klang, 57000, Penang",
        //         TelephoneNumber = "03-12345678",
        //         FaxNumber = "03-12345679",
        //         PICName = "Lee Jia Jun",
        //         HandphoneNumber = "012-3456789",
        //         EmailAddress = "lee@hotmail.com.my",
        //         CCEmailAddress = "finance@hotmail.com.my",
        //         CLEKmailNotification = "operater@hotmail.com"
        //     }
        //     );
        // modelBuilder.Entity<User>().HasData(
        //     new User
        //     {
        //         UserId = "MNG00001",
        //         Password = "123456",
        //         FullName = "Pradeep",
        //         CompanyName = "ABC Forwarders",
        //         CompanyCode = "A0001",
        //         Access = "CLE & ALE",
        //         AccessLevel = "Full-Access",
        //         EmailAddress = "deep@gmail.com",
        //         ContactNumber = "0123456789",
        //         Status = "Active",
        //         UpdatedBy = "System"
        //     },
        //     new User()
        //     {
        //         UserId = "STF00001",
        //         Password = "123456",
        //         FullName = "Thanesh",
        //         CompanyName = "ABC Forwarders",
        //         CompanyCode = "A0001",
        //         Access = "CLE",
        //         AccessLevel = "Half-Access",
        //         EmailAddress = "nesh@gmail.com",
        //         ContactNumber = "0123456789",
        //         Status = "Active",
        //         UpdatedBy = "System"
        //     },
        //     new User()
        //     {
        //         UserId = "MNG00002",
        //         Password = "123456",
        //         FullName = "Tristen",
        //         CompanyName = "ABC Haulier",
        //         CompanyCode = "A0002",
        //         Access = "CLE & ALE",
        //         AccessLevel = "Full-Access",
        //         EmailAddress = "tristen@hotmail.com",
        //         ContactNumber = "0123456789",
        //         Status = "Active",
        //         UpdatedBy = "System"
        //     },
        //     new User()
        //     {
        //         UserId = "STF0002",
        //         Password = "123456",
        //         FullName = "Vincent",
        //         CompanyName = "ABC Haulier",
        //         CompanyCode = "A0002",
        //         Access = "ALE",
        //         AccessLevel = "Full-Access",
        //         EmailAddress = "vincent@hotmail.com",
        //         ContactNumber = "0123456789",
        //         Status = "Active",
        //         UpdatedBy = "System"
        //     }
        //     );
    }
}
