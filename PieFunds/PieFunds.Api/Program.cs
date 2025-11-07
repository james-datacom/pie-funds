using FluentValidation;
using FluentValidation.AspNetCore;
using PieFunds.Application.Common.Models;
using PieFunds.Application.Features.Users.Queries.GetUserByEmail;
using PieFunds.Application.Interfaces;
using PieFunds.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// MediatR registration: scan Application assembl
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetUserByEmailQuery).Assembly));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GetUserByEmailQueryValidator>();

// Repos / Infrastructure
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
