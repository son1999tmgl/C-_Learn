using Microsoft.EntityFrameworkCore;
using WebApi.Controllers.Data;
using WebApi.Controllers.Services;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DTMyDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"));
});


//DI
builder.Services.AddScoped<ILoaiReponsitory, LoaiReponsitoryMemory>();
builder.Services.AddScoped<IHangHoaReponsitory, HangHoaReponsitory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
