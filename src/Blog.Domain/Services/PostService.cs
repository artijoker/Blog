using Blog.Domain.Entities;
using Blog.Domain.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class PostService
    {
        private readonly IUnitOfWork _unit;


        public PostService(IUnitOfWork unit)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<PostModelV1> GetPostById(int postId)
        {
            var post = await _unit.PostRepository.GetById(postId);
            return new PostModelV1
            {
                Id = post.Id,
                Title = post.Title,
                Anons = post.Anons,
                FullText = post.FullText,
                LastChange = post.LastChange,
                Status = post.Status,
                AccountId = post.AccountId,
                CategoryId = post.CategoryId,
                AccountLogin = post.Account!.Login,
                CategoryName = post.Сategory!.Name
            };
        }

        public async Task<IReadOnlyList<PostModelV1>> GetRecentPublishedPosts(int number)
            => PostToPostModel(await _unit.PostRepository.GetRecentPosts(number));


        public async Task<IReadOnlyList<PostModelV1>> GetPendingPosts()
            => PostToPostModel(await _unit.PostRepository.GetPostsByStatus(PostStatus.Pending));

        public async Task<IReadOnlyList<PostModelV1>> GetPublishedPosts()
            => PostToPostModel(await _unit.PostRepository.GetPostsByStatus(PostStatus.Publish));



        public async Task<IReadOnlyList<PostModelV1>> GetPublishedPostsByAccountId(int accountId)
            => PostToPostModel(await _unit.PostRepository.GetPostsByAccountIdAndStatus(accountId, PostStatus.Publish));


        public async Task<IReadOnlyList<PostModelV1>> GetDraftPostsByAccountId(int accountId)
            => PostToPostModel(await _unit.PostRepository.GetPostsByAccountIdAndStatus(accountId, PostStatus.Draft));

        public async Task<IReadOnlyList<PostModelV1>> GetPendingPostsByAccountId(int accountId)
            => PostToPostModel(await _unit.PostRepository.GetPostsByAccountIdAndStatus(accountId, PostStatus.Pending));

        public async Task<IReadOnlyList<PostModelV1>> GetRejectedPostsByAccountId(int accountId)
             => PostToPostModel(await _unit.PostRepository.GetPostsByAccountIdAndStatus(accountId, PostStatus.Rejected));



        public async Task<IReadOnlyList<PostModelV1>> GetPublishedPostsByCategoryId(int categoryId)
            => PostToPostModel(await _unit.PostRepository.GetPostsByCategoryIdAndStatus(categoryId, PostStatus.Publish));


        public async Task<IReadOnlyList<Post>> GetDraftPostsByCategoryId(int categoryId)
            => await _unit.PostRepository.GetPostsByCategoryIdAndStatus(categoryId, PostStatus.Draft);

        public async Task<IReadOnlyList<Post>> GetPendingPostsByCategoryId(int categoryId)
            => await _unit.PostRepository.GetPostsByCategoryIdAndStatus(categoryId, PostStatus.Pending);

        public async Task<IReadOnlyList<Post>> GetRejectedPostsByCategoryId(int categoryId)
           => await _unit.PostRepository.GetPostsByCategoryIdAndStatus(categoryId, PostStatus.Rejected);



        public async Task<IReadOnlyList<Post>> GetPublishedPostsWhichVisibleAll()
            => await _unit.PostRepository.GetPostsByStatusAndWhichVisibleAll(PostStatus.Publish);

        public async Task<IReadOnlyList<Post>> GetPublishedPostsWhichVisibleAllByAccountId(int accountId)
            => await _unit.PostRepository.GetPostsByAccountIdAndStatusWhichVisibleAll(accountId, PostStatus.Publish);

        public async Task<IReadOnlyList<Post>> GetPublishedPostsWhichVisibleAllByCategoryId(int categoryId)
            => await _unit.PostRepository.GetPostsByCategoryIdAndStatusWhichVisibleAll(categoryId, PostStatus.Publish);


        private async Task AddPost(int accountId,
            string title,
            string anons,
            string fullText,
            bool isVisibleEveryone,
            bool isAllowCommenting,
            int categoryId,
            string status)
        {
            var category = await _unit.CategoryRepository.GetById(categoryId);
            var post = new Post()
            {
                Title = title,
                Anons = anons,
                FullText = fullText,
                LastChange = DateTime.Now,
                IsVisibleAll = isVisibleEveryone,
                IsAllowCommenting = isAllowCommenting,
                CategoryId = category.Id,
                AccountId = accountId,
                Status = status
            };

            await _unit.PostRepository.Add(post);
            await _unit.SaveChangesAsync();
        }

        public async Task AddNewPost(
            int accountId,
            string title,
            string anons,
            string fullText,
            bool isVisibleEveryone,
            bool isAllowCommenting,
            int categoryId)
        {
            await AddPost(accountId, title, anons, fullText, isVisibleEveryone, isAllowCommenting, categoryId, PostStatus.Draft);
        }

        public async Task AddPostAndSendToModeration(
            int accountId,
            string title,
            string anons,
            string fullText,
            bool isVisibleEveryone,
            bool isAllowCommenting,
            int categoryId)
        {

            await AddPost(accountId, title, anons, fullText, isVisibleEveryone, isAllowCommenting, categoryId, PostStatus.Pending);
        }

        public async Task AddPostAndPublished(
            int accountId,
            string title,
            string anons,
            string fullText,
            bool isVisibleEveryone,
            bool isAllowCommenting,
            int categoryId)
        {
            await AddPost(accountId, title, anons, fullText, isVisibleEveryone, isAllowCommenting, categoryId, PostStatus.Publish);
        }


        public async Task<Post> UpdatePost(
            int postId,
            string title,
            string anons,
            string fullText,
            bool isVisibleEveryone,
            bool isAllowCommenting,
            int categoryId)
        {
            var post = await _unit.PostRepository.GetById(postId);

            if (post.Title != title || post.Anons != anons || post.FullText != fullText || post.CategoryId != categoryId)
            {
                post.Title = title;
                post.Anons = anons;
                post.FullText = fullText;
                post.IsVisibleAll = isVisibleEveryone;
                post.IsAllowCommenting = isAllowCommenting;
                post.CategoryId = categoryId;

                post.Status = post.Status == PostStatus.Publish ? PostStatus.Pending : post.Status;

                post.LastChange = DateTime.Now;

                await _unit.PostRepository.Update(post);
                await _unit.SaveChangesAsync();
            }
            return post;
        }


        public async Task<Post> AdminUpdatePost(
            int postId,
            string title,
            string anons,
            string fullText,
            bool isVisibleEveryone,
            bool isAllowCommenting,
            int categoryId)
        {
            var post = await _unit.PostRepository.GetById(postId);

            if (post.Title != title || post.Anons != anons || post.FullText != fullText || post.CategoryId != categoryId)
            {
                post.Title = title;
                post.Anons = anons;
                post.FullText = fullText;
                post.IsVisibleAll = isVisibleEveryone;
                post.IsAllowCommenting = isAllowCommenting;
                post.CategoryId = categoryId;


                post.LastChange = DateTime.Now;

                await _unit.PostRepository.Update(post);
                await _unit.SaveChangesAsync();
            }
            return post;
        }

        public async Task RemovePost(int postId)
        {
            var post = await _unit.PostRepository.GetById(postId);
            await _unit.PostRepository.Remove(post);
            await _unit.SaveChangesAsync();
        }

        public async Task<Post> PublishPost(int postId)
            => await UpdatePostStatus(postId, PostStatus.Publish);

        public async Task<Post> RejectPost(int postId)
            => await UpdatePostStatus(postId, PostStatus.Rejected);

        public async Task<Post> SendPostModeration(int postId)
            => await UpdatePostStatus(postId, PostStatus.Pending);

        public async Task<Post> SendPostToDraft(int postId)
            => await UpdatePostStatus(postId, PostStatus.Draft);


        private async Task<Post> UpdatePostStatus(int postId, string status)
        {
            var post = await _unit.PostRepository.GetById(postId);
            if (post.Status != status)
            {
                post.LastChange = DateTime.Now;
                post.Status = status;
                await _unit.PostRepository.Update(post);
                await _unit.SaveChangesAsync();
            }
            return post;
        }

        private static IReadOnlyList<PostModelV1> PostToPostModel(IReadOnlyList<Post> posts)
        {
            return posts.Select(p => new PostModelV1
            {
                Id = p.Id,
                Title = p.Title,
                Anons = p.Anons,
                LastChange = p.LastChange,
                Status = p.Status,
                AccountId = p.AccountId,
                AccountLogin = p.Account!.Login,
                CategoryId = p.CategoryId,
                CategoryName = p.Сategory!.Name
            }).ToList();
        }
    }
}
