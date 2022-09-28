using Blog.Domain.Entities;
using Blog.Domain.Services;
using Blog.HttpModels.Requests.Account.V1;
using Blog.HttpModels.Requests.Account.V2;

using Blog.HttpModels.Requests.Authentication.V1;
using Blog.HttpModels.Responses;
using Blog.HttpModels.Responses.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data;
using System.Security.Claims;
using Blog.Domain.Models;
using Microsoft.Net.Http.Headers;

namespace Blog.Server.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly RegistrationService _registrationService;
        private readonly AuthorizationService _authorizationService;

        public AccountController(
            AccountService accountService,
            RegistrationService registrationService,
            AuthorizationService authorizationService)
        {
            _accountService = accountService;
            _registrationService = registrationService;
            _authorizationService = authorizationService;
        }

        [HttpGet("get-accounts-and-quantity-published-posts")]
        public async Task<ActionResult<Response<IReadOnlyList<AccountModelV2>>>> GetAccountsAndCountPublishedPostsForEachAccountWhereQuantityGreaterZero()
        {
            return new Response<IReadOnlyList<AccountModelV2>>()
            {
                Succeeded = true,
                Result = await _accountService.GetAccountsAndCountPublishedPostsForEachAccountWhereQuantityGreaterZero()
            };
        }

        [HttpPost("registration")]
        public async Task<ActionResult<LogInResponse<AccountModelV3>>> UserRegisterAccount(AccountRequestModelV1 model)
        {
            var (account, token) = await _registrationService.UserRegisterAccount(model.Email, model.Login, model.Password);
            return new LogInResponse<AccountModelV3>()
            {
                Succeeded = true,
                Token = token,
                Result = account
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<LogInResponse<AccountModelV1>>> LogIn(LogInRequestModelV1 model)
        {
            var (account, token) = await _authorizationService.Authorize(model.Login, model.Password);


            return new LogInResponse<AccountModelV1>()
            {
                Succeeded = true,
                Result = account,
                Token = token
            };
        }


        //var role = User.FindFirstValue(ClaimTypes.Role);
        [Authorize]
        [HttpGet("get-account")]
        public async Task<ActionResult<Response<AccountModelV3>>> GetAccount()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return new Response<AccountModelV3>()
            {
                Succeeded = true,
                Result = await _accountService.GetByIdAccountInfo(id)
            };
        }

        [Authorize(Roles = "user")]
        [HttpPost("edit-account")]
        public async Task<ActionResult<Response<object>>> UserUpdateAccount(AccountUpdateRequestModelV1 model)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _accountService.UserUpdateAccount(id, model.Email, model.Login, model.NewPassword);

            return new Response<object>()
            {
                Succeeded = true,
            };
        }

        
        

        [Authorize]
        [HttpGet("delete-account")]
        public async Task<ActionResult<Response<object>>> RemoveAccount()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _accountService.RemoveAccount(id);
            return new Response<object>()
            {
                Succeeded = true
            };
        }


        [Authorize(Roles = "admin")]
        [HttpPost("admin/add-account")]
        public async Task<ActionResult<Response<object>>> AddAccount(AccountRequestModelV2 model)
        {
            await _registrationService.AdminRegisterAccount(model.Email, model.Login, model.Password, model.RoleId);

            return new Response<object>()
            {
                Succeeded = true
            };
        }


        [Authorize(Roles = "admin")]
        [HttpPost("admin/edit-account")]
        public async Task<ActionResult<Response<object>>> AdminUpdateAccount(AccountUpdateRequestModelV2 model)
        {
            await _accountService.AdminUpdateAccount(model.AccountId, model.Email, model.Login,  model.RoleId, model.NewPassword);

            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [Authorize(Roles = "admin")]
        [HttpPost("admin/delete-account")]
        public async Task<ActionResult<Response<object>>> RemoveAccount([FromBody] int accountId)
        {
            await _accountService.RemoveAccount(accountId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [Authorize(Roles = "admin")]
        [HttpPost("admin/banned-account")]
        public async Task<ActionResult<Response<object>>> BannedAccount([FromBody] int accountId)
        {
            await _accountService.BanAccount(accountId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [Authorize(Roles = "admin")]
        [HttpPost("admin/unlock-account")]
        public async Task<ActionResult<Response<object>>> UnlockAccount([FromBody] int accountId)
        {
            await _accountService.UnlockAccount(accountId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin/get-all-accounts")]
        public async Task<ActionResult<Response<IReadOnlyList<Account>>>> GetAllAccounts()
        {
            return new Response<IReadOnlyList<Account>>()
            {
                Succeeded = true,
                Result = await _accountService.GetAll()
            };
        }

        [Authorize(Roles = "admin")]
        [HttpPost("admin/get-account-by-id")]
        public async Task<ActionResult<Response<AccountModelV3>>> GetAccountsById([FromBody] int accountId)
        {
            return new Response<AccountModelV3>()
            {
                Succeeded = true,
                Result = await _accountService.GetByIdAccountInfo(accountId)
            };
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin/get-all-accounts-and-quantity-published-posts")]
        public async Task<ActionResult<Response<IReadOnlyList<AccountModelV3>>>> GetAccountsAndCountPublishedPostsForEachAccount()
        {
            return new Response<IReadOnlyList<AccountModelV3>>()
            {
                Succeeded = true,
                Result = await _accountService.GetAccountsAndCountPublishedPostsForEachAccount()
            };
        }



    }
}
