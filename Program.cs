using CateringManagement.Application.Interfaces;
using CateringManagement.Application.Services;
using CateringManagement.Domain.Entities;
using CateringManagement.Infrastructure.Data;
using CateringManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CateringDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "CateringDbConnection")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// Infrastructure Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IVendorRepository,VendorRepository>();

builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped< IHotelRepository, HotelRepository>();

builder.Services.AddScoped<IEmployeeRateHistoryRepository,EmployeeRateHistoryRepository>();
builder.Services.AddScoped<IEmployeeRateHistoryService,EmployeeRateHistoryService>();

builder.Services.AddScoped<IVendorRateHistoryRepository,VendorRateHistoryRepository>();
builder.Services.AddScoped< IVendorRateHistoryService, VendorRateHistoryService>();

//builder.Services.AddScoped<IRateMastersRepository, RateMastersRepository>();
//builder.Services.AddScoped<IRateMastersService, RateMastersService>();

builder.Services.AddScoped<IShiftRepository, ShiftRepository>();

builder.Services.AddScoped<IDailyHotelAssignmentService, DailyHotelAssignmentService>();
builder.Services.AddScoped<IDailyHotelAssignmentRepository, DailyHotelAssignmentRepository>();

builder.Services.AddScoped<IHotelRateHistoryService,HotelRateHistoryService>();
builder.Services.AddScoped<IHotelRateHistoryRepository, HotelRateHistoryRepository>();

builder.Services.AddScoped< IDailyEmployeePaymentRepository, DailyEmployeePaymentRepository>();
builder.Services.AddScoped<
    IDailyEmployeePaymentService,
    DailyEmployeePaymentService>();
builder.Services.AddScoped<
    IEmployeeRateHistoryRepository,
    EmployeeRateHistoryRepository>();

builder.Services.AddScoped<
    IVendorRateHistoryRepository,
    VendorRateHistoryRepository>();

builder.Services.AddScoped<
    IVendorSettlementRepository,
    VendorSettlementRepository>();
builder.Services.AddScoped< IVendorSettlementService, VendorSettlementService>();
builder.Services.AddScoped<IHotelBillRepository, HotelBillRepository>();
builder.Services.AddScoped<IHotelBillingService, HotelBillingService>();
builder.Services.AddScoped<
    IHotelCollectionRepository,
    HotelCollectionRepository>();

builder.Services.AddScoped<
    IHotelCollectionService,
    HotelCollectionService>();
builder.Services.AddScoped<
    IEmployeeAdvanceRepository,
    EmployeeAdvanceRepository>();

builder.Services.AddScoped<
    IEmployeeAdvanceService,
    EmployeeAdvanceService>();
builder.Services.AddScoped<IHotelBillingService, HotelBillingService>();
// Register repository
builder.Services.AddScoped<
    IEmployeePaymentRepository,
    EmployeePaymentRepository>();

// Register service
builder.Services.AddScoped<
    IEmployeePaymentService,
    EmployeePaymentService>();

// Vendor payment repository & service
builder.Services.AddScoped<
    IVendorPaymentRepository,
    VendorPaymentRepository>();
builder.Services.AddScoped<
    IVendorPaymentService,
    VendorPaymentService>();

// Repository — add this registration
builder.Services.AddScoped<
    IRateMastersRepository,
    RateMastersRepository>();
builder.Services.AddScoped<
    IVendorAdvanceRepository,
    VendorAdvanceRepository>();

builder.Services.AddScoped<
    IVendorAdvanceService,
    VendorAdvanceService>();
builder.Services.AddScoped<
    IHotelAssignmentDataRepository,
    HotelAssignmentDataRepository>();

builder.Services.AddScoped<
    IHotelAssignmentDataService,
    HotelAssignmentDataService>();
builder.Services.AddScoped<
    IDailySupplierAssignmentToVendorRepository,
    DailySupplierAssignmentToVendorRepository>();

builder.Services.AddScoped<
    IDailySupplierAssignmentToVendorService,
    DailySupplierAssignmentToVendorService>();

// Authentication Repository
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// Authentication Service
builder.Services.AddScoped<IAuthService, AuthService>();

// Password Hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:7174")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");

var jwtKey = jwtSettings["Key"]
    ?? throw new InvalidOperationException("JWT Key is missing.");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name,
            ClockSkew = TimeSpan.Zero
        };
    });
// Permission-based authorization
builder.Services.AddAuthorization(options =>
{
    string[] permissions =
    {
        "Dashboard.View",

        "Employee.View",
        "Employee.Create",
        "Employee.Edit",
        "Employee.Delete",

        "Vendor.View",
        "Vendor.Create",
        "Vendor.Edit",

        "Hotel.View",
        "Hotel.Create",
        "Hotel.Edit",

        "Assignment.View",
        "Assignment.Create",
        "Assignment.Edit",
        "Assignment.Delete",

        "Payment.View",
        "Payment.Create",
        "Payment.Edit",

        "Reports.View",
        "Reports.Export",

        "Role.View",
        "Role.Manage",

        "User.View",
        "User.Create",
        "User.Edit",
        "User.Delete"
    };

    foreach (var permission in permissions)
    {
        options.AddPolicy(permission, policy =>
        {
            policy.RequireClaim("permission", permission);
        });
    }
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//------------------------------------------------------------------------------------------------


//// TEMPORARY PASSWORD HASH GENERATION
//using (var scope = app.Services.CreateScope())
//{
//    var hasher = scope.ServiceProvider
//        .GetRequiredService<IPasswordHasher<User>>();

//    var user = new User
//    {
//        UserId = 1,
//        FullName = "Amol",
//        Email = "giriamolp@gmail.com"
//    };

//    var hash = hasher.HashPassword(user, "Amol@1234");

//    Console.WriteLine("NEW PASSWORD HASH:");
//    Console.WriteLine(hash);
//}

// Existing middleware below
app.UseCors("ReactPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();



//------------------------------------------------------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("ReactPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
