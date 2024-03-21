using Microsoft.AspNetCore.Identity;

namespace BankingSystem.API.Models
{
    public class UserRoles : IdentityRole<Guid>
    {
        public const string AccountHolder = "AccountHolder";
        public const string TellerPerson = "TellerPerson";
        //public const string SuperAdmin = "SuperAdmin";
    }
}