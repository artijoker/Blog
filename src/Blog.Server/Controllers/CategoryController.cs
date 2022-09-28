using Blog.Domain.Entities;
using Blog.Domain.Models;
using Blog.Domain.Services;
using Blog.HttpModels.Requests.Category.V1;
using Blog.HttpModels.Requests.Category.V2;
using Blog.HttpModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Server.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }


        [HttpGet("get-categories")]
        public async Task<ActionResult<Response<IReadOnlyList<CategoryModelV1>>>> GetCategories()
        {
            return new Response<IReadOnlyList<CategoryModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetCategories()
            };
        }

        [HttpGet("get-categories-and-count-published-posts")]
        public async Task<ActionResult<Response<IReadOnlyList<CategoryModelV2>>>> GetCategoriesAndCountPublishedPostsForEachCategory()
        {
            return new Response<IReadOnlyList<CategoryModelV2>>()
            {
                Succeeded = true,
                Result = await _service.GetCategoriesAndCountPublishedPostsForEachCategory()
            };
        }

        [HttpPost("admin/add-category")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<object>>> AddCategory(CategoryRequestModelV1 model)
        {
            await _service.AddCategory(model.CategoryName);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("admin/edit-category")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<object>>> UpdateCategory(CategoryRequestModelV2 model)
        {
            await _service.UpdateCategory(model.CategoryId, model.CategoryName);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("admin/delete-category")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<object>>> RemoveCategory([FromBody] int id)
        {
            await _service.RemoveCategory(id);
            return new Response<object>()
            {
                Succeeded = true
            };
        }
    }
}


