using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Category;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public CategoryService(ICategoryRepository categoryRepository,
                               IActionContextAccessor actionContextAccessor,
                               IUnitOfWork unitOfWork,
                               IFileService fileService)
        {
            _categoryRepository = categoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<CategoryIndexVM> GetAllAsync()
        {
            var model = new CategoryIndexVM();
            model.Categories = await _categoryRepository.GetAllAsync();
            return model;
        }
        public async Task<bool> CreateAsync(CategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.CoverPhoto))
            {
                _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.CoverPhoto, 3000))
            {
                _modelState.AddModelError("Photoname", "Sekilin olcusu 3000kb dan boyukdur");
                return false;
            }

            var category = new Category
            {
                Name = model.Name,
                CoverPhoto = _fileService.Upload(model.CoverPhoto),
                CreatedAt = DateTime.Now,
            };

            await _categoryRepository.CreateAsync(category);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<CategoryUpdateVM?> GetUpdateAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null) return null;

            var model = new CategoryUpdateVM
            {
                Name = category.Name,
                CoverPhotoPath = category.CoverPhoto
            };
            return model;
        }

        public async Task<bool> PostUpdateAsync(int id, CategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                _modelState.AddModelError(string.Empty, "Category not found");
                return false;
            }

            if (model.CoverPhoto is not null)
            {
                if (!_fileService.IsImage(model.CoverPhoto))
                {
                    _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                    return false;
                }

                if (!_fileService.IsBiggerThanSize(model.CoverPhoto, 3000))
                {
                    _modelState.AddModelError("Photoname", "Sekilin olcusu 3000kb dan boyukdur");
                    return false;
                }
                //kohne sekili silmek
                _fileService.Delete(category.CoverPhoto);
                category.CoverPhoto = _fileService.Upload(model.CoverPhoto);
            }

            category.Name = model.Name;
            category.ModifiedAt = DateTime.Now;

            _categoryRepository.Update(category);  
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                _modelState.AddModelError(string.Empty, "Category not found");
                return false;
            }

            category.IsDeleted = true;

            _categoryRepository.Delete(category);
            await _unitOfWork.CommitAsync();
            return true;
        }

       

        
    }
}
