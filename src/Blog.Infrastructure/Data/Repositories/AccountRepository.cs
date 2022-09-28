using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data.Repositories
{
    public class AccountRepository : EfRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Account> GetById(int id)
        {
            return Entities.Where(a => a.Id == id).Include(a => a.Role).FirstAsync();
        }

        public override Task<IReadOnlyList<Account>> GetAll()
        {
            return base.GetAll();
        }
        public async Task<IReadOnlyList<Tuple<Account, int>>> GetAccountsAndCountPostsForEachAccountByStatus(string status)
        {
            return await Entities.Include(a => a.Role)
                .Select(a => new Tuple<Account, int>(a, a.Posts.Where(p => p.Status == status).Count())).ToListAsync();
        }

        public async Task<IReadOnlyList<Tuple<Account, int>>> GetAccountsAndCountPostsForEachAccountByStatusWhereQuantityGreaterThan(string status, int quantity)
        {
            return await Entities.Include(a => a.Role).Where(a => a.Posts.Count > quantity)
                .Select(a => new Tuple<Account, int>(a, a.Posts.Where(p => p.Status == status).Count()))
                .ToListAsync();
        }
        public async Task<IReadOnlyList<Tuple<Account, int>>> GetAllAuthorsAndQuantityPostsEachHas()
        {
            return await Entities.Include(a => a.Role)
                .Select(a => new Tuple<Account, int>(a, a.Posts.Where(p => p.Status == PostStatus.Publish).Count()))
                .ToListAsync();
        }


        public Task<Account?> FindByLogin(string login)
        {
            return Entities.Where(a => a.Login == login).Include(a => a.Role).FirstOrDefaultAsync();
        }


        public Task<Account> GetByEmail(string email)
        {
            return Entities.Where(a => a.Email == email).Include(a => a.Role).FirstAsync();
        }

        public Task<Account> GetByLogin(string login)
        {
            return Entities.Where(a => a.Login == login).Include(a => a.Role).FirstAsync();
        }

        public Task<bool> IsUniqueEmail(string email)
        {
            return Entities.AnyAsync(a => a.Email == email);
        }

        public Task<bool> IsUniqueLogin(string login)
        {
            return Entities.AnyAsync(a => a.Login == login);
        }

        public override Task Remove(Account entity)
        {
            return Task.CompletedTask;
        }

    }
}
