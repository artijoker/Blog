using Blog.Domain.Exceptions;
using Blog.Domain.Services;
using Blog.HttpModels.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Blog.Server.Middlewares
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, JwtTokenService service)
        {
            var token = context.Request.Headers[HeaderNames.Authorization].FirstOrDefault();
            
            if (token is not null)
                if (await service.IsBlockToken(token.Split(' ')[1]))
                {
                    //throw new BlockedTokenException($"Token '{token}' blocked");
                    await context.Response.WriteAsJsonAsync(new Response<object>()
                    {
                        Succeeded = false,
                        Message = $"Токен доступа недействителен"
                        
                    });
                    return;
                }
 
            await _next(context);
        }
    }
}
