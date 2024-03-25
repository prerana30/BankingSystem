using BankingSystem.API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BankingSystem.API.Utilities
{
    public static class TellerPersonAuthorizationPolicyExtensions
    {
        public static void AddTellerPersonAuthorizationPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("TellerPersonPolicy", policy =>
                {
                    policy.RequireRole("TellerPerson");
                    policy.Requirements.Add(new TellerPersonRequirement());
                });
            });

            services.AddTransient<IAuthorizationHandler, TellerPersonAuthorizationHandler>();
        }
    }
}
