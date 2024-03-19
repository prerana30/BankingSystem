using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using BankingSystem.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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
            if (await userService.GetUsersAsync() == null)
            {
                var list = new List<Users>();
                return list;
            }

            return Ok(await userService.GetUsersAsync());
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
        public async Task<ActionResult<Users>> Login(string email, string password)
        {
            var user = await userService.LoginUser(email, password);
            if (user == null)
            {
                // return NotFound("Email or Password is incorrect.");
                return StatusCode(400, "Email or Password is incorrect.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUsers(UserDTO user)
        {
            var users = await userService.AddUsers(user);
            if (users == null)
            {
                return StatusCode(400, "User already exists.");
            }
            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(Guid userId)
        {
            userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<Users>> UpdateUsers(Guid userId, UserDTO user)
        {
            var newUser = await userService.UpdateUsersAsync(userId, user);
            if (newUser == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(newUser);
        }

        [HttpPatch("{userId}")]
        public async Task<ActionResult<Users>> PatchUserDetails(Guid userId, JsonPatchDocument<UserDTO> patchDocument)
        {
            var user = await userService.PatchUserDetails(userId, patchDocument);
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
