using Microsoft.EntityFrameworkCore;
using TableServe.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TableServeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TableServeDb")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.UseCors(); // ← uncomment this when React front end calls the API

app.Run();
