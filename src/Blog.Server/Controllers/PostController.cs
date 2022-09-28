using Blog.Domain.Entities;
using Blog.Domain.Models;
using Blog.Domain.Services;
using Blog.HttpModels.Requests.Post.V1;
using Blog.HttpModels.Requests.Post.V2;
using Blog.HttpModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Server.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _service;

        public PostController(PostService service)
        {
            _service = service;
        }


        [HttpGet("get-posts")]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetPublishedPosts()
        {
            var result = await _service.GetPublishedPosts();

            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = result
            };
        }

        [HttpPost("get-post")]
        public async Task<ActionResult<Response<PostModelV1>>> GetPost([FromBody] int postId)
        {
            var result = await _service.GetPostById(postId);

            return new Response<PostModelV1>()
            {
                Succeeded = true,
                Result = result
            };
        }

        [HttpPost("get-posts-by-accountId")]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetPostsByAccountId([FromBody] int accountId)
        {
            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetPublishedPostsByAccountId(accountId)
            };
        }


        [HttpGet("get-published-posts-by-accountId")]
        [Authorize]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetPublishedPostsByAccountId()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetPublishedPostsByAccountId(id)
            };
        }


        [HttpGet("get-draft-posts-by-accountId")]
        [Authorize]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetDraftPostsByAccountId()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetDraftPostsByAccountId(id)
            };
        }

        [HttpGet("get-pending-posts-by-accountId")]
        [Authorize]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetPendingPostsByAccountId()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetPendingPostsByAccountId(id)
            };
        }

        [HttpGet("get-rejected-posts-by-accountId")]
        [Authorize]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetRejectedPostsByAccountId()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetRejectedPostsByAccountId(id)
            };
        }


        [HttpPost("get-posts-by-categoryId")]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetPostsByCategoryId([FromBody] int categoryId)
        {
            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetPublishedPostsByCategoryId(categoryId)
            };
        }

        [HttpPost("get-recent-posts")]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetRecentPublishedPosts([FromBody] int number)
        {

            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetRecentPublishedPosts(number)
            };
        }



        [HttpPost("add-post")]
        [Authorize]
        public async Task<ActionResult<Response<object>>> AddPost(PostRequestsModelV1 model)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.AddNewPost(id, model.Title, model.Anons, model.FullText, true, false, model.CategoryId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("update-post")]
        [Authorize]
        public async Task<ActionResult<Response<object>>> UpdatePost(PostRequestsModelV2 model)
        {
            await _service.UpdatePost(model.PostId, model.Title, model.Anons, model.FullText, true, false, model.CategoryId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("delete-post")]
        [Authorize]
        public async Task<ActionResult<Response<object>>> RemovePost([FromBody] int postId)
        {

            await _service.RemovePost(postId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }



        [HttpPost("add-post-and-send-to-moderation")]
        [Authorize]
        public async Task<ActionResult<Response<object>>> AddPostAndSendToModeration(PostRequestsModelV1 model)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.AddPostAndSendToModeration(id, model.Title, model.Anons, model.FullText, true, false, model.CategoryId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("send-post-moderation")]
        [Authorize]
        public async Task<ActionResult<Response<object>>> SendPostModeration([FromBody] int postId)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.SendPostModeration(postId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("admin/add-post-and-published")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<object>>> AddPostAndPublished(PostRequestsModelV1 model)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.AddPostAndPublished(id, model.Title, model.Anons, model.FullText, true, false, model.CategoryId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }



        [HttpGet("admin/get-pending-posts")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<IReadOnlyList<PostModelV1>>>> GetPendingPosts()
        {
            return new Response<IReadOnlyList<PostModelV1>>()
            {
                Succeeded = true,
                Result = await _service.GetPendingPosts()
            };
        }

        
        [HttpPost("send-post-to-draft")]
        [Authorize]
        public async Task<ActionResult<Response<object>>> SendPostToDraft([FromBody] int postId)
        {

            await _service.SendPostToDraft(postId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }


        [HttpPost("publish-post")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<object>>> PublishPost([FromBody] int postId)
        {

            await _service.PublishPost(postId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("reject-post")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<object>>> RejectPost([FromBody] int postId)
        {

            await _service.RejectPost(postId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }

        [HttpPost("admin/update-post")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Response<object>>> AdminUpdatePost(PostRequestsModelV2 model)
        {
            await _service.AdminUpdatePost(model.PostId, model.Title, model.Anons, model.FullText, true, false, model.CategoryId);
            return new Response<object>()
            {
                Succeeded = true
            };
        }
    }
}
