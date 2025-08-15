using Api.Exceptions.Handler;
using Api.Security.Extensions;
using Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddOpenApi();

            services.AddCors(options =>
            {
                options.AddPolicy("react-policy", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithOrigins("http://localhost:3000");
                });
            });

            services.AddMediatR(config => config
                .RegisterServicesFromAssembly(typeof(GetTopicsHandler).Assembly));

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddIdentityServices(configuration);
            return services;
        }

        public static async Task<WebApplication> UseApiServices(
            this WebApplication app)
        {
            app.UseCors("react-policy");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                await app.InitializeDatabaseAsync();
            }

            app.UseExceptionHandler(options => { });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
