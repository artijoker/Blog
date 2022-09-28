using Blog.Domain.Services;
using Blog.HttpModels.Responses;
using Blog.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace Blog.Server.Filters
{
    public class JwtTokenFilter : IAuthorizationFilter, IOrderedFilter
    {

        private readonly JwtTokenService _service;
        public int Order { get; } = 0;

        public JwtTokenFilter(JwtTokenService service)
        {
            _service = service;
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

            if (token is not null)
            {
                var res = await _service.IsBlockToken(token.Split(' ')[1]);

                if (res)
                {
                    context.Result = new ObjectResult(
                        new Response<object>()
                        {
                            Succeeded = false,
                            Message = "Токен доступа недействителен!"
                        });
                }
            }
        }
    }
}
