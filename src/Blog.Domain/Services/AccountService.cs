using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class AccountService
    {
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher<Account> _hasher;

        public AccountService(
            IUnitOfWork unit,
            IPasswordHasher<Account> hasher)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public Task<IReadOnlyList<Account>> GetAll()
        {
            return _unit.AccountRepository.GetAll();
        }

        public async Task<AccountModelV3> GetByIdAccountInfo(int id)
        {
            var account = await _unit.AccountRepository.GetById(id);
            return new AccountModelV3()
            {
                Id = account.Id,
                Email = account.Email,
                Login = account.Login,
                Registered = account.Registered,
                QuantityPosts = await _unit.PostRepository.GetQuantityPostsByAccountId(account.Id),
                roleId = account.RoleId,
                roleName = account.Role!.Name,
                IsBanned = account.IsBanned,
                IsDeleted = account.IsDeleted
            };
        }

        public async Task RemoveAccount(int id)
        {
            if (id == 1)
                throw new OperationLockException();

            var account = await _unit.AccountRepository.GetById(id);
            await BlockAllTokensForAccountId(account.Id);
            account.IsDeleted = true;
            await _unit.AccountRepository.Update(account);
            await _unit.SaveChangesAsync();
        }

        public async Task BanAccount(int id)
        {
            if (id == 1)
                throw new OperationLockException();
            var account = await _unit.AccountRepository.GetById(id);
            account.IsBanned = true;

            await BlockAllTokensForAccountId(account.Id);
            await _unit.AccountRepository.Update(account);
            await _unit.SaveChangesAsync();
        }

        public async Task UnlockAccount(int id)
        {
            var account = await _unit.AccountRepository.GetById(id);
            account.IsBanned = false;
            await _unit.AccountRepository.Update(account);
            await _unit.SaveChangesAsync();
        }

        public async Task UserUpdateAccount(int accountId, string email, string login, string? newPassword = null)
        {
            bool isModified = false;
            var account = await _unit.AccountRepository.GetById(accountId);

            if (account.Email != email)
            {
                if (await _unit.AccountRepository.IsUniqueEmail(email))
                    throw new DuplicateEmailException();
                account.Email = email;
                isModified = true;
            }

            if (account.Login != login)
            {
                if (await _unit.AccountRepository.IsUniqueLogin(login))
                    throw new DuplicateLoginException();
                account.Login = login;
                isModified = true;
            }

            if (newPassword is not null)
            {
                account.PasswordHash = _hasher.HashPassword(account, newPassword);
                isModified = true;
            }

            if (isModified)
            {
                await _unit.AccountRepository.Update(account);
                await _unit.SaveChangesAsync();
            }
        }


        public async Task AdminUpdateAccount(int accountId, string email, string login, int roleId, string? newPassword = null)
        {
            bool isModified = false;
            var account = await _unit.AccountRepository.GetById(accountId);

            if (account.Email != email)
            {
                if (await _unit.AccountRepository.IsUniqueEmail(email))
                    throw new DuplicateEmailException();
                account.Email = email;
                isModified = true;
            }

            if (account.Login != login)
            {
                if (await _unit.AccountRepository.IsUniqueLogin(login))
                    throw new DuplicateLoginException();
                account.Login = login;
                isModified = true;
            }

            if (newPassword is not null)
            {
                account.PasswordHash = _hasher.HashPassword(account, newPassword);
                isModified = true;
            }

            if (roleId != account.RoleId)
            {
                if (accountId == 1)
                    throw new OperationLockException();
                account.RoleId = roleId;
                await BlockAllTokensForAccountId(account.Id);
                isModified = true;
            }

            if (isModified)
            {
                await _unit.AccountRepository.Update(account);
                await _unit.SaveChangesAsync();
            }

        }

        private async Task BlockAllTokensForAccountId(int accountId)
        {
            var tokens = await _unit.JwtTokenRepository.GetAllTokenByAccountId(accountId);

            foreach (var token in tokens)
                token.IsBlocked = true;

            await _unit.JwtTokenRepository.UpdateRange(tokens.ToArray());
        }


        public async Task<IReadOnlyList<AccountModelV3>> GetAccountsAndCountPublishedPostsForEachAccount()
        {
            var tuples = await _unit.AccountRepository.GetAccountsAndCountPostsForEachAccountByStatus(PostStatus.Publish);
            return tuples.Select(t =>new AccountModelV3()
            {
                Id = t.Item1.Id,
                Email = t.Item1.Email,
                Login = t.Item1.Login,
                Registered = t.Item1.Registered,
                QuantityPosts = t.Item2,
                roleId = t.Item1.RoleId,
                roleName = t.Item1.Role!.Name,
                IsBanned = t.Item1.IsBanned,
                IsDeleted = t.Item1.IsDeleted
            }).ToList();
        }
        //GetAccountsAndCountPublishedPostsForEachAccountWhereQuantityGreaterThan
        public async Task<IReadOnlyList<AccountModelV2>> GetAccountsAndCountPublishedPostsForEachAccountWhereQuantityGreaterZero()
        {
            var tuples = await _unit.AccountRepository.GetAccountsAndCountPostsForEachAccountByStatusWhereQuantityGreaterThan(PostStatus.Publish,0);
            return tuples.Select(t =>
            new AccountModelV2()
            {
                Id = t.Item1.Id,
                Login = t.Item1.Login,
                QuantityPosts = t.Item2
            }).ToList();
        }
    }
}
