using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unit;

        public CategoryService(IUnitOfWork unit)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }


        public async Task<IReadOnlyList<CategoryModelV1>> GetCategories()
        {
            var categories = await _unit.CategoryRepository.GetAll();
            return categories.Select(c => new CategoryModelV1()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

        }

        public async Task<IReadOnlyList<CategoryModelV2>> GetCategoriesAndCountPublishedPostsForEachCategory()
        {
            var tuples = await _unit.CategoryRepository.GetCategoriesAndCountPostsForEachCategoryByStatys(PostStatus.Publish);
            return tuples.Select(t => new CategoryModelV2
            {
                Id = t.Item1.Id,
                Name = t.Item1.Name,
                QuantityPosts = t.Item2
            }).ToList();
        }

        public async Task AddCategory(string name)
        {
            if (await _unit.CategoryRepository.IsUniqueName(name))
                throw new DuplicateCategoryException();

            var category = new Category()
            {
                Name = name
            };

            await _unit.CategoryRepository.Add(category);
            await _unit.SaveChangesAsync();
        }

        public async Task UpdateCategory(int id, string name)
        {
            if (id == 1)
                throw new DefaultCategoryException();

            if (await _unit.CategoryRepository.IsUniqueName(name))
                throw new DuplicateCategoryException();

            var category = await _unit.CategoryRepository.GetById(id);

            category.Name = name;

            await _unit.CategoryRepository.Update(category);
            await _unit.SaveChangesAsync();
        }

        public async Task RemoveCategory(int id)
        {
            if (id == 1)
                throw new DefaultCategoryException();

            var category = await _unit.CategoryRepository.GetById(id);

            var posts = await _unit.PostRepository.GetPostsByCategoryId(category.Id);
            foreach (var post in posts)
                post.CategoryId = 1;

            await _unit.PostRepository.UpdateRange(posts.ToArray());

            await _unit.CategoryRepository.Remove(category);
            await _unit.SaveChangesAsync();
        }

    }

}
