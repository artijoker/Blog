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
    public class RegistrationService
    {
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher<Account> _hasher;
        private readonly ITokenService _tokenService;

        public RegistrationService(
            IUnitOfWork unit,
            IPasswordHasher<Account> hasher,
            ITokenService tokenService)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<(AccountModelV3, string)> UserRegisterAccount(string email, string login, string password)
        {
            var role = await _unit.RoleRepository.GetRoleUser();
            var account = await CreateAccount(email, login, password, role);
            await _unit.AccountRepository.Add(account);

            await _unit.SaveChangesAsync();

            account = await _unit.AccountRepository.GetByEmail(email);

            var jwtToken = new JwtToken
            {
                Token = _tokenService.GenerateToken(account.Id, role.Name),
                AccountId = account.Id
            };
            account.Tokens.Add(jwtToken);

            await _unit.AccountRepository.Update(account);
            await _unit.JwtTokenRepository.Add(jwtToken);

            await _unit.SaveChangesAsync();

            return (new AccountModelV3()
            {
                Id = account.Id,
                Email = account.Email,
                Login =     account.Login,
                Registered = account.Registered,
                roleId = account.RoleId,
                roleName = role.Name,
                QuantityPosts = 0
            }, jwtToken.Token);
        }

        public async Task AdminRegisterAccount(string email, string login, string password, int roleId)
        {
            var role = await _unit.RoleRepository.GetById(roleId);
            var account = await CreateAccount(email, login, password, role);
            await _unit.AccountRepository.Add(account);
            await _unit.SaveChangesAsync();
        }

        private async Task<Account> CreateAccount(string email, string login, string password, Role role)
        {
            if (await _unit.AccountRepository.IsUniqueEmail(email))
                throw new DuplicateEmailException();

            if (await _unit.AccountRepository.IsUniqueLogin(login))
                throw new DuplicateLoginException();

            Account account = new()
            {
                Login = login,
                Email = email,
                RoleId = role.Id,
                Role = role,
                Registered = DateTime.Now
            };

            account.PasswordHash = _hasher.HashPassword(account, password);

            return account;
        }
    }
}
