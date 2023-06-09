﻿using Application.Core.Commands.RecommendationCommands;
using Application.Core.Interfaces;
using Application.Core.Services;
using Domain.Core.Interfaces;
using Infrastructure.Core.Repositories;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins(
                "http://localhost:8080",
                "https://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(CreateRecommendationCommandHandler));

builder.Services.AddControllers();
builder.Services.AddTransient<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<IFriendRepository, FriendRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();

builder.Services.AddTransient<IRecommendationCrudService, RecommendationCrudService>();
builder.Services.AddTransient<IUserCrudService, UserCrudService>();
builder.Services.AddTransient<ICityCrudService, CityCrudService>();
builder.Services.AddTransient<ITagCrudService, TagCrudService>();
builder.Services.AddTransient<IFriendCrudService, FriendCrudService>();
builder.Services.AddTransient<INotificationCrudService, NotificationCrudService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recommendoo", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
app.UseCors();

app.MapControllers();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Recommendoo API");
});

app.Run();

#pragma warning disable S3903
#pragma warning disable S1118
public partial class Program { }