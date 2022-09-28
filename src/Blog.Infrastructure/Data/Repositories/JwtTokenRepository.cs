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
    public class JwtTokenRepository : EfRepository<JwtToken>, IJwtTokenRepository
    {
        public JwtTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<JwtToken>> GetAllTokenByAccountId(int accountId)
        {
            return await Entities.Where(e => e.AccountId == accountId).ToListAsync();
        }

        public override async Task<JwtToken?> FindById(int id)
        {
            return await Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public override async Task<IReadOnlyList<JwtToken>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public override async Task<JwtToken> GetById(int Id)
        {
            return await Entities.FirstAsync(it => it.Id == Id);
        }

        public Task<bool> IsBlockToken(string token)
        {
            return Entities.Where(t => t.Token == token).Select(t => t.IsBlocked).FirstAsync();
        }
    }
}
