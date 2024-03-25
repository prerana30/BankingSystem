using BankingSystem.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BankingSystem.API.Utilities
{
    public class TellerPersonRequirement : IAuthorizationRequirement
    {
        // No additional properties needed for this requirement
    }

    public class TellerPersonAuthorizationHandler : AuthorizationHandler<TellerPersonRequirement>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public TellerPersonAuthorizationHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TellerPersonRequirement requirement)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var userRole = Enum.GetName(typeof(UserRoles), UserRoles.TellerPerson);

                if (await _roleManager.RoleExistsAsync(userRole))
                {
                    if (context.User.IsInRole(userRole))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    else
                    {
                        throw new Exception("User is unauthorized!");
                    }
                }
            }

            // If the user does not meet the requirement, fail the authorization
            context.Fail();
        }
    }
}
