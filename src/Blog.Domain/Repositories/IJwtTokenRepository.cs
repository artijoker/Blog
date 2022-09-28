using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories
{
    public interface IJwtTokenRepository : IRepository<JwtToken>
    {
        Task<IReadOnlyList<JwtToken>> GetAllTokenByAccountId(int accountId);

        Task<bool> IsBlockToken(string token);
    }
}
