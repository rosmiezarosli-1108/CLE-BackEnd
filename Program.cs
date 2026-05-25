using System.Text;
using CLE_BackEnd.Services;
using CLE_BackEnd.Data;
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
    // Cleaned: Removed manual CORS headers here because app.UseCors("AllowReact") 
    // globally takes care of it for all static assets and endpoints.
});

app.MapControllers();
app.Run();