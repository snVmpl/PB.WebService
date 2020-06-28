using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace PB.WebService.Security
{
    public class ParityHandler : AuthorizationHandler<ParityRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ParityRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated || !context.User.HasClaim(c => c.Type == requirement.ClaimKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var clientId = context.User.FindFirst(c => c.Type == requirement.ClaimKey);

            if (requirement.ValidateParity(clientId.Value))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
