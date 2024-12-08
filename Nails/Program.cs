using Microsoft.EntityFrameworkCore;
using Nails.Interface;
using Nails.Models;
using System.Collections.Generic;
using static Nails.Interface.IUser;

var builder = WebApplication.CreateBuilder(args);

var connectingString = builder.Configuration.GetConnectionString("ConnectDb");
builder.Services.AddDbContext <ÍîãòèContext> (options => options.UseSqlServer(connectingString));

builder.Services.AddTransient<IUser, ClientClass>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
