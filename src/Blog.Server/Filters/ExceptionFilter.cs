using Blog.Domain.Exceptions;
using Blog.HttpModels.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Server.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {

        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionFilter(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            var message = TryGetMessageFromException(context);
            if (message is not null)
            {
                context.Result = new ObjectResult(
                    new Response<object>()
                    {
                        Succeeded = false,
                        Message = message
                    });
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new ObjectResult(
                    new Response<object>()
                    {
                        Succeeded = false,
                        Bug = true,
                        Message = "Неизвестная ошибка"
                    });
            }


        }


        private static string? TryGetMessageFromException(ExceptionContext context)
        {
            return context.Exception switch
            {
                DuplicateEmailException => "Пользователь с таким email уже существует",
                DuplicateLoginException => "Пользователь с таким логином уже существует",
                InvalidLoginException => "Неверный логин",
                InvalidPasswordException => "Неверный пароль",
                BannedAccountException => "Аккаунт заблокирован",
                DeletedAccountException => "Аккаунт удален",
                NotFoundRoleException => "Роль не найдена",
                DuplicateRoleException => "Роль с таким названием уже существует",
                DuplicateCategoryException => "Категория с таким названием уже существует",
                OperationLockException => "Операция была заблокирована сервером",
                DefaultRoleException => "Нельзя удалять базовые роли",
                DefaultCategoryException => "Нельзя удалять базовую категорию",
                BlockedTokenException => "Токен доступа заблокирован",
                _ => null
            };
        }
    }
}
