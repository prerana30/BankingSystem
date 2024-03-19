using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BankingSystem.API.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsersAsync();
        Task<Users?> GetUserAsync(Guid userId);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<Users> AddUsers(Users users);
        Task<Users> UpdateUsersAsync(Guid userId, Users users);
        void DeleteUser(Guid userId);
        Task<Users> PatchUserDetails(Guid userId, JsonPatchDocument<UserDTO> userDetails);
    }
}
