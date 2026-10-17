using CateringManagement.Application.Interfaces;
using CateringManagement.Application.Services;
using CateringManagement.Infrastructure.Data;
using CateringManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


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
