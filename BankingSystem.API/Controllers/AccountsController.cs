using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Repository;
using BankingSystem.API.Models;
using BankingSystem.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTful_API__ASP.NET_Core.Repository;

namespace BankingSystem.API.Controllers
{

    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountServices accountServices;
        private readonly UserService userServices;

        public AccountsController(AccountServices AccountServices, UserService UserService)
        {
            accountServices = AccountServices ?? throw new ArgumentOutOfRangeException(nameof(AccountServices));
            userServices = UserService ?? throw new ArgumentOutOfRangeException(nameof(UserService));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accounts>>> GetAccounts()
        {
            if (await accountServices.GetAccountsAsync() == null)
            {
                var list = new List<Accounts>();
                return list;
            }

            return Ok(await accountServices.GetAccountsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Accounts>> GetAccountAsync(Guid id)
        {
            var account = await accountServices.GetAccountAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<Accounts>> AddAccounts(AccountDTO account, string email)
        {

            var user = await userServices.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userId = user.UserId;

            var checkAccount = await accountServices.GetAccountByUserIdAsync(userId);
            if (checkAccount != null)
            {
                return StatusCode(400, "User already has an account.");
            }
            var accounts = await accountServices.AddAccounts(account);
            if (accounts == null)
            {
                return StatusCode(400, "User already exists.");
            }
            return Ok(accounts);

        }

        [HttpDelete("{accountId}")]
        public ActionResult DeleteUser(Guid accountId)
        {
            accountServices.DeleteAccount(accountId);
            return NoContent();
        }

        [HttpPut("{accountId}")]
        public async Task<ActionResult<Accounts>> UpdateAccounts(Guid accountId, AccountDTO account)
        {
            var newAccount = await accountServices.UpdateAccountsAsync(accountId, account);
            if (newAccount == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(newAccount);
        }

        [HttpPatch("{userId}")]
        public async Task<ActionResult<Accounts>> PatchAccountDetails(Guid accountId, JsonPatchDocument<AccountDTO> patchDocument)
        {
            var account = await accountServices.PatchAccountDetails(accountId, patchDocument);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(account))
            {
                return BadRequest(ModelState);
            }
            if (account == null)
            {
                NotFound();
            }
            return Ok(account);
        }

    }
}
