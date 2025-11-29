using ECommerceAPI.DataSource;
using ECommerceAPI.Models.Options;
using ECommerceAPI.Services;
using ECommerceAPI.Services.Files;
using FileSignatures;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<CommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommerceContext")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFileSeeder, FileSeeder>();

builder.Services.Configure<SeedDataOptions>(builder.Configuration.GetSection("SeedData"));

builder.Services.AddSingleton<IFileFormatInspector>(new FileFormatInspector([new TextDocument()]));
builder.Services.AddSingleton<IDbInitialzer, DbInitializer>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<IDbInitialzer>().Initialize();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();