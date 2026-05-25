using System.Text;
using CLE_BackEnd.Services;
using CLE_BackEnd.Data;
using CLE_BackEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. FIXED CORS POLICY: Support both the main vercel domain AND its subdomains
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://cle-front-end.vercel.app"
            )
            .SetIsOriginAllowedToAllowWildcardSubdomains() // Handles automatic Vercel preview URLs if needed
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            // Add this line to ensure preflight requests are cached and cleared properly
            .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["userToken"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// Scoped Services
builder.Services.AddScoped<IAssignedHaulierService, AssignedHaulierService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IContainerAddressService, ContainerAddressService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingDocumentService, BookingDocumentService>();
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IPrimeMoverService, PrimeMoverService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ITrailerService, TrailerService>();
builder.Services.AddScoped<IContainerAuditService, ContainerAuditService>();
builder.Services.AddScoped<IAleContainerAuditService, AleContainerAuditService>();
builder.Services.AddScoped<IAleContainerAddressService, AleContainerAddressService>();
builder.Services.AddScoped<IAleContainerService, AleContainerService>();
builder.Services.AddScoped<IAleBookingService, AleBookingService>();
builder.Services.AddScoped<IAleBookingDocumentService, AleBookingDocumentService>();
builder.Services.AddScoped<IAleAssignedHaulierService, AleAssignedHaulierService>();
builder.Services.AddScoped<IAleTimeSlotService, AleTimeSlotService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();

// --- AUTOMATIC PROGRAMMATIC SEEDING SYSTEM START ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        // Force the structural creation of database tables
        context.Database.EnsureCreated();

        // FORCE RESET: Directly clear out any legacy text rows that crash the JSON reader
        Console.WriteLine("Executing clean database reset...");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Users\" CASCADE;");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Companies\" CASCADE;");

        // Safe JSON column mapping data seeder for Companies
        context.Companies.AddRange(
            new Company
            {
                CompanyCode = "A0001",
                CompanyName = "ABC Forwarders",
                SSMNo = "123456-A",
                SSTNo = "W10-1234-5678",
                Role = "Forwarder",
                Region = new List<SystemRegion>
                {
                    new SystemRegion { SystemName = "CLE", RegionCode = "PEN" },
                    new SystemRegion { SystemName = "ALE", RegionCode = "PEN" }
                },
                ManagerName = "Pradeep",
                Address = "Butterworth, 13000, Penang",
                TelephoneNumber = "03-12345678",
                FaxNumber = "03-12345679",
                PICName = "Thanesh",
                HandphoneNumber = "012-3456789",
                EmailAddress = "nesh@gmail.com.my",
                CCEmailAddress = "finance@gmail.com.my",
                CLEKmailNotification = "operater@gmail.com"
            },
            new Company
            {
                CompanyCode = "A0002",
                CompanyName = "ABC Haulier",
                SSMNo = "123456-B",
                SSTNo = "W11-1234-5678",
                Role = "Haulier",
                Region = new List<SystemRegion>
                {
                    new SystemRegion { SystemName = "CLE", RegionCode = "PEN" },
                    new SystemRegion { SystemName = "ALE", RegionCode = "PEN" }
                },
                ManagerName = "Tristen",
                Address = "Port Klang, 57000, Penang",
                TelephoneNumber = "03-12345678",
                FaxNumber = "03-12345679",
                PICName = "Lee Jia Jun",
                HandphoneNumber = "012-3456789",
                EmailAddress = "lee@hotmail.com.my",
                CCEmailAddress = "finance@hotmail.com.my",
                CLEKmailNotification = "operater@hotmail.com"
            }
        );
        context.SaveChanges();

        // Safe seeder for User Profiles
        context.Users.AddRange(
            new User
            {
                UserId = "MNG00001",
                Password = "123456",
                FullName = "Pradeep",
                CompanyName = "ABC Forwarders",
                CompanyCode = "A0001",
                Access = "ALE",
                AccessLevel = "Full-Access",
                EmailAddress = "deep@gmail.com",
                ContactNumber = "0123456789",
                Status = "Active",
                UpdatedBy = "System"
            },
            new User
            {
                UserId = "STF00001",
                Password = "123456",
                FullName = "Thanesh",
                CompanyName = "ABC Forwarders",
                CompanyCode = "A0001",
                Access = "ALE",
                AccessLevel = "Half-Access",
                EmailAddress = "nesh@gmail.com",
                ContactNumber = "0123456789",
                Status = "Active",
                UpdatedBy = "System"
            },
            new User
            {
                UserId = "MNG00002",
                Password = "123456",
                FullName = "Tristen",
                CompanyName = "ABC Haulier",
                CompanyCode = "A0002",
                Access = "ALE",
                AccessLevel = "Full-Access",
                EmailAddress = "tristen@hotmail.com",
                ContactNumber = "0123456789",
                Status = "Active",
                UpdatedBy = "System"
            },
            new User
            {
                UserId = "STF00002",
                Password = "123456",
                FullName = "Vincent",
                CompanyName = "ABC Haulier",
                CompanyCode = "A0002",
                Access = "ALE",
                AccessLevel = "Full-Access",
                EmailAddress = "vincent@hotmail.com",
                ContactNumber = "0123456789",
                Status = "Active",
                UpdatedBy = "System"
            }
        );
        context.SaveChanges();
        Console.WriteLine("Database seed replaced with proper JSON formatting successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database initialization issue: {ex.Message}");
    }
}
// --- AUTOMATIC PROGRAMMATIC SEEDING SYSTEM END ---

// Force headers manually for every single incoming request
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Access-Control-Allow-Origin", "https://cle-front-end.vercel.app");
    context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
    context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");

    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("OK");
        return;
    }

    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. CRITICAL FIX: Move Routing and CORS to the absolute top of the request pipeline
app.UseRouting(); 
app.UseCors("AllowReact"); 

app.UseAuthentication();
app.UseAuthorization();

var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/api/uploads"
});

app.MapControllers();
app.Run();