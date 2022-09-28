using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class JwtTokenService
    {
        private readonly IUnitOfWork _unit;

        public JwtTokenService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<bool> IsBlockToken(string token)
        {
            var res = await _unit.JwtTokenRepository.IsBlockToken(token);
            await _unit.SaveChangesAsync();
            return res;
        }
    }
}
