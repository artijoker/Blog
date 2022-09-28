using Blog.Domain.Entities;
using Blog.Domain.Services;
using Blog.HttpModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Server.Controllers
{
    [Route("role")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _service;

        public RoleController(RoleService service)
        {
            _service = service;
        }

        [HttpGet("admin/get-roles")]
        public async Task<ActionResult<Response<IReadOnlyList<Role>>>> GetRoles()
        {
            return new Response<IReadOnlyList<Role>>()
            {
                Succeeded = true,
                Result = await _service.GetAll()
            };
        }
    }
}
