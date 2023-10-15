using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Blog;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Users
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogIndexVM> IndexGetAllAsync()
        {
            var model = new BlogIndexVM()
            {
                Blogs = await _blogRepository.GetAllAsync()
            };

            return model;
        }
        public async Task<BlogDetailsVM>? DetailsAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null) return null;

            var blogSingle = new BlogDetailsVM
            {
                Title = blog.Title,
                Description = blog.Description,
                ShortDesc = blog.ShortDesc,
                Author = blog.Author,
                Category = blog.Category,
                Photo = blog.Photo,
                Tags = blog.Tags,
                CreatedAt = blog.CreatedAt,
                ModifiedAt = blog.ModifiedAt,
            };

            return blogSingle;
        }
    }
}
