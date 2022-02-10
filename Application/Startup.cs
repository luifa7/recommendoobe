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
            services.AddMediatR(typeof(CreateCityCommandHandler));
            services.AddMediatR(typeof(CreateUserCommandHandler));
            services.AddMediatR(typeof(CreateTagCommandHandler));
            services.AddMediatR(typeof(CreatePendingFriendCommandHandler));
            services.AddMediatR(typeof(CreateNotificationCommandHandler));

            services.AddMediatR(typeof(DeleteRecommendationCommandHandler));
            services.AddMediatR(typeof(DeleteCityCommandHandler));
            services.AddMediatR(typeof(DeleteUserCommandHandler));
            services.AddMediatR(typeof(DeleteFriendCommandHandler));
            services.AddMediatR(typeof(DeleteNotificationCommandHandler));

            services.AddMediatR(typeof(AcceptFriendRequestCommandHandler));
            services.AddMediatR(typeof(UpdateCityCommandHandler));
            services.AddMediatR(typeof(UpdateRecommendationCommandHandler));
            services.AddMediatR(typeof(UpdateUserCommandHandler));
            services.AddMediatR(typeof(MarkNotificationAsOpenedCommandHandler));

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
                    await context.Response.WriteAsync("Recommendoo API");
                });
            });
        }
    }
}
