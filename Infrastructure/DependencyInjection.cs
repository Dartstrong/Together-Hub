using Application.Data.DataBaseContext;
using Application.Security.Services;
using Infrastructure.Data.DataBaseContext;
using Infrastructure.Security.Auth;
using Infrastructure.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString(
                "SqLiteConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IJwtSecurityService, JwtSecurityService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsTopicAuthor", policy =>
                {
                    policy.Requirements.Add(new TopicDeletionRequirement());
                });
            });

            services.AddTransient<IAuthorizationHandler, TopicDeletionRequirementHandler>();

            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
            return services;
        }
    }
}
