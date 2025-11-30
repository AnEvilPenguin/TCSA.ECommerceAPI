using ECommerceAPI.DataSource;
using ECommerceAPI.Models.Options;
using ECommerceAPI.Services;
using ECommerceAPI.Services.Files;
using FileSignatures;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<CommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommerceContext")));
builder.Services.AddScoped<IBlobService>(options => 
    new BlobService(builder.Configuration.GetConnectionString("AzureBlob")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFileSeeder, FileSeeder>();
builder.Services.AddScoped<ICsvSeeder, CsvSeeder>();
builder.Services.AddScoped<IExcelSeeder, ExcelSeeder>();
builder.Services.AddScoped<IExcelReader, ExcelReader>();

QuestPDF.Settings.License = LicenseType.Community;
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.Configure<SeedDataOptions>(builder.Configuration.GetSection("SeedData"));

var assembly = typeof(TextDocument).Assembly;
var allFormats = FileFormatLocator.GetFormats(assembly, true);

builder.Services.AddSingleton<IFileFormatInspector>(new FileFormatInspector(allFormats));
builder.Services.AddSingleton<IDbInitialzer, DbInitializer>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<IDbInitialzer>().Initialize();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();