using Application.Data.DataBaseContext;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Security.Auth
{
    public class TopicDeletionRequirement : IAuthorizationRequirement { }

    public class TopicDeletionRequirementHandler(
        IApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
        : AuthorizationHandler<TopicDeletionRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TopicDeletionRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                context.Fail();
                return;
            }

            var routeValue = httpContextAccessor.HttpContext?.Request.RouteValues;
            var value = routeValue
                 ?.FirstOrDefault(x => x.Key == "id")
                .Value?.ToString();

            if (String.IsNullOrEmpty(value))
            {
                context.Fail();
                return;
            }

            var topicId = TopicId.Of(Guid.Parse(value));

            var relationship = await dbContext.Relationships
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.UserReference == userId.ToString() &&
                    r.TopicReference == topicId);

            if (relationship?.Role == ParticipantRole.Organizer)
            {
                context.Succeed(requirement);
            }   
        }
    }
}
