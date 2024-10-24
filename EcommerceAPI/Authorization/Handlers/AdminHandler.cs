using EcommerceAPI.Authorization.Requirements;
using EcommerceAPI.Entity.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EcommerceAPI.Authorization.Handlers
{
    public class AdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role)?.Value;
            if (roleClaim != null && roleClaim == nameof(Role.SUPER_ADMIN))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                throw new UnauthorizedAccessException("Please provided valid token.");
            }
            return Task.CompletedTask;
        }
    }
}
