using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BankingSystem.API.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsersAsync();
        Task<Users?> GetUserAsync(int userId);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<Users> AddUsers(Users users);
        Task<Users> UpdateUsersAsync(int userId, Users users);
        void DeleteUser(int userId);
        Task<Users> PatchUserDetails(int userId, JsonPatchDocument<UserDTO> userDetails);
    }
}
