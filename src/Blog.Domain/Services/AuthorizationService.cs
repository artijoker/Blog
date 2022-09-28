using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class AuthorizationService
    {
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher<Account> _hasher;
        private readonly ITokenService _tokenService;

        public AuthorizationService(
            IUnitOfWork unit,
            IPasswordHasher<Account> hasher,
            ITokenService tokenService)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<(AccountModelV1, string)> Authorize(string login, string password)
        {
            var account = await _unit.AccountRepository.FindByLogin(login);

            if (account == null)
                throw new InvalidLoginException();

            var result = _hasher.VerifyHashedPassword(
                account, account.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
                throw new InvalidPasswordException();

            if (account.IsDeleted)
                throw new DeletedAccountException();

            if (account.IsBanned)
                throw new BannedAccountException();

            var jwtToken = new JwtToken
            {
                Token = _tokenService.GenerateToken(account.Id, account.Role!.Name),
                AccountId = account.Id
            };

            await _unit.JwtTokenRepository.Add(jwtToken);
            await _unit.SaveChangesAsync();


            return (new AccountModelV1()
            {
                Id = account.Id,
                Email = account.Email,
                Registered = account.Registered,
                Login = account.Login,
            }, jwtToken.Token);
        }

    }
}
