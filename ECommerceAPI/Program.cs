using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommerceContext")));
    
var app = builder.Build();

app.Run();