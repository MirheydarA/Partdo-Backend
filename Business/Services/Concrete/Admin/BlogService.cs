using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Blog;
using Business.ViewModels.Admin.Product;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public BlogService(IBlogRepository blogRepository,
                              IActionContextAccessor actionContextAccessor,
                              IUnitOfWork unitOfWork,
                              IFileService fileService)
        {
            _blogRepository = blogRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<BlogIndexVM> IndexGetAllAsync()
        {
            var model = new BlogIndexVM();
            model.Blogs = await _blogRepository.GetAllAsync();
            return model;
        }
        public async Task<bool> PostCreateAsync(BlogCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photos", "File is not in image format");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.Photo, 3000))
            {
                _modelState.AddModelError("Photos", "Image is bigger than 3000kb");
                return false;
            }

            if (!await _blogRepository.GetBlogByTitle(model.Title))
            {
                _modelState.AddModelError("Title", "This already used");
                return false;
            }

            var blog = new Blog
            {
                Title = model.Title,
                Description = model.Description,
                ShortDesc = model.ShortDesc,
                Category = model.Category,
                Tags = model.Tags,
                Author = model.Author,
                Photo = _fileService.Upload(model.Photo),
                CreatedAt = DateTime.Now,
            };

            await _blogRepository.CreateAsync(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<BlogUpdateVM?> GetUpdateAsync(int id)
        {
            var blog =  await _blogRepository.GetByIdAsync(id);
            if (blog is null) return null;


            var model = new BlogUpdateVM
            {
                Category = blog.Category,
                Title = blog.Title,
                Description = blog.Description,
                ShortDesc= blog.ShortDesc,
                Tags = blog.Tags,
                Author = blog.Author,
                PhotoPath = blog.Photo
            };

            return model;
        }
        public async Task<bool> PostUpdateAsync(int id, BlogUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var blog = await _blogRepository.GetByIdAsync(id);

            if (blog is null)
            {
                _modelState.AddModelError(string.Empty, "Blog Not Found!");
                return false;
            }

            if (model.Photo is not null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                    return false;
                }

                if (!_fileService.IsBiggerThanSize(model.Photo, 3000))
                {
                    _modelState.AddModelError("Photoname", "Sekilin olcusu 3000kb dan boyukdur");
                    return false;
                }
                //kohne sekili silmek
                _fileService.Delete(model.PhotoPath);
                blog.Photo = _fileService.Upload(model.Photo);
            }

            blog.Title = model.Title;
            blog.Description = model.Description;
            blog.ShortDesc = model.ShortDesc;
            blog.Tags = model.Tags;
            blog.Category = model.Category;
            blog.Author = model.Author;
            blog.Photo = model.PhotoPath;
            blog.ModifiedAt = DateTime.Now;


            _blogRepository.Update(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
            {
                _modelState.AddModelError(string.Empty, "Blog Not Found!");
                return false;
            }
            blog.IsDeleted = true;

            _blogRepository.Delete(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<BlogDetailsVM> DetailsAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null) return null;

            var selectedBlog = new BlogDetailsVM()
            {
                Title = blog.Title,
                Description = blog.Description,
                Category = blog.Category,
                ShortDesc = blog.ShortDesc,
                Tags = blog.Tags,
                Author = blog.Author,
                CreatedAt = blog.CreatedAt,
                ModifiedAt = blog.ModifiedAt,
                Photo = blog.Photo
            };

            return selectedBlog;
        }
    }
}
