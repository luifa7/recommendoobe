using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Application.Commands;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Application.Services;
using Microsoft.OpenApi.Models;

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
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080",
                                            "https://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                    });
            });

            // TODO: only one is needed
            services.AddMediatR(typeof(CreateRecommendationCommandHandler));

            services.AddControllers();
            services.AddTransient<IRecommendationRepository, RecommendationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IFriendRepository, FriendRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();

            services.AddTransient<RecommendationCRUDService>();
            services.AddTransient<UserCRUDService>();
            services.AddTransient<CityCRUDService>();
            services.AddTransient<TagCRUDService>();
            services.AddTransient<FriendCRUDService>();
            services.AddTransient<NotificationCRUDService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recommendoo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Recommendoo API");
                });
            });
        }
    }
}
