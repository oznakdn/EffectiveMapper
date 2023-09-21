using Example.Api.Data.Context;
using Example.Api.Data.Database;
using Gleeman.EffectiveMapper.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEffectiveMapper();
builder.Services.AddDbContext<AppDbContext>(option=> option.UseInMemoryDatabase("TestDb"));

var app = builder.Build();
DatabaseInitializer.DataInitialize(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
