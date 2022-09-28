using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class CommentService
    {
        private readonly IUnitOfWork _unit;

        public CommentService(IUnitOfWork unit)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<IReadOnlyList<Comment>> AddComment(int accountId, int postId, string text)
        {
            var comment = new Comment()
            {
                AccountId = accountId,
                PostId = postId,
                Text = text,
                Sent = DateTime.Now
            };

            await _unit.CommentRepository.Add(comment);
            await _unit.SaveChangesAsync();

            return await GetCommentsByPostId(postId);
        }

        public async Task<IReadOnlyList<Comment>> UpdateComment(int accountId, int postId, string text)
        {
            var comment = await _unit.CommentRepository.GetCommentsByAccountIdAndPostId(accountId, postId);
            comment.Text = text;
            comment.Sent = DateTime.Now;
            await _unit.CommentRepository.Update(comment);
            await _unit.SaveChangesAsync();

            return await GetCommentsByPostId(postId);
        }

        public async Task<IReadOnlyList<Comment>> RemoveComment(int accountId, int postId)
        {
            var comment = await _unit.CommentRepository.GetCommentsByAccountIdAndPostId(accountId, postId);

            await _unit.CommentRepository.Remove(comment);
            await _unit.SaveChangesAsync();

            return await GetCommentsByPostId(postId);
        }


        public async Task<IReadOnlyList<Comment>> GetCommentsByPostId(int postId)
            => await _unit.CommentRepository.GetCommentsByPostId(postId);
        

        public async Task<IReadOnlyList<Comment>> GetCommentsByAccountId(int accountId)
            => await _unit.CommentRepository.GetCommentsByAccountId(accountId);

    }
}
