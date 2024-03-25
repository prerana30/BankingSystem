using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using BankingSystem.API.Services;
using BankingSystem.API.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingSystem.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        public UsersController(UserService UserService)
        {
            userService = UserService ?? throw new ArgumentOutOfRangeException(nameof(UserService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var users = await userService.GetUsersAsync();
            if (users == null)
            {
                var list = new List<Users>();
                return list;
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(Guid id)
        {
            var user = await userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserInfoDisplayDTO>> Login(string username, string password)
        {
            var user = await userService.Login(username, password);
            if (user == null)
            {
                // return NotFound("Email or Password is incorrect.");
                return StatusCode(400, "Email or Password is incorrect.");
            }
            // Check if the user is authenticated: storing current logged in user
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the user's username
                Constants.user = user;
                Constants.userId = user.Id;
                Constants.role= User.FindFirstValue(ClaimTypes.Role);
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUsers(UserCreationDTO user)
        {
            var users = await userService.RegisterUser(user);
            if (users == null)
            {
                return StatusCode(400, "User already exists.");
            }
            return Ok(users);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteUser(Guid Id)
        {
            userService.DeleteUser(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Users>> UpdateUsers(Guid Id, UserUpdateDTO user)
        {
            var newUser = await userService.UpdateUsersAsync(Id, user);
            if (newUser == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(newUser);
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult<Users>> PatchUserDetails(Guid Id, JsonPatchDocument<UserCreationDTO> patchDocument)
        {
            var user = await userService.PatchUserDetails(Id, patchDocument);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(user))
            {
                return BadRequest(ModelState);
            }
            if (user == null)
            {
                NotFound();
            }
            return Ok(user);
        }
    }
}
