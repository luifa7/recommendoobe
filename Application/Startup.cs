﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Application.Commands;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Application.Services;

namespace Application
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                                  {
                                      builder.WithOrigins("http://localhost:8080",
                                                          "https://localhost:8080")
                                      .SetIsOriginAllowedToAllowWildcardSubdomains();
                                  });
            });

            services.AddMediatR(typeof(CreateRecommendationCommandHandler));
            services.AddControllers();
            services.AddTransient<IRecommendationRepository, RecommendationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<RecommendationCRUDService>();
            services.AddTransient<UserCRUDService>();
            services.AddTransient<CityCRUDService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
