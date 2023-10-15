using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.SubCategory;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Services.Concrete.Admin
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private ModelStateDictionary _modelState;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository,
                                  IActionContextAccessor actionContextAccessor,
                                  IUnitOfWork unitOfWork,
                                  ICategoryRepository categoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<SubCategoryIndexVM> IndexGetAllAsync()
        {
            var model = new SubCategoryIndexVM();
            model.SubCategories = await _subCategoryRepository.GetSubCategoriesWithCategory();
            return model;
        }
        public async Task<SubCategoryCreateVM> GetCreateAsync()
        {
            var subCategories = await _subCategoryRepository.GetSubCategoriesWithCategory();
            var model = new SubCategoryCreateVM();
            var listCategory = await _categoryRepository.GetAllAsync();

            model.Categories = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return model;
        }
        public async Task<bool> PostCreateAsync(SubCategoryCreateVM model)
        {
            var listCategory = await _categoryRepository.GetAllAsync();

            model.Categories = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            if (!_modelState.IsValid) return false;

            var subCategory = new SubCategory()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now,
            };

            if (!await _subCategoryRepository.GetByName(subCategory.Name))
            {
                _modelState.AddModelError("Name", "This name already used");
                return false;
            }

            await _subCategoryRepository.CreateAsync(subCategory);
            await _unitOfWork.CommitAsync();
            return true;

        }
        public async Task<SubCategoryUpdateVM?> GetUpdateAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdWithInclude(id);
            if (subCategory == null) return null;

            var listCategory = await _categoryRepository.GetAllAsync();

            var model = new SubCategoryUpdateVM
            {
                Categories = listCategory.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),

                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
            };

            return model;
        }
        public async Task<bool> PostUpdateAsync(int id, SubCategoryUpdateVM model)
        {
            var listCategory = await _categoryRepository.GetAllAsync();

            var categories = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            if (!_modelState.IsValid) return false;

            var subCategory = await _subCategoryRepository.GetByIdAsync(id);
            if (subCategory is null)
            {
                _modelState.AddModelError(string.Empty, "Subcategory Not Found!");
                return false;
            }

            var category = await _categoryRepository.GetByIdAsync(model.CategoryId);
            if (category is null)
            {
                _modelState.AddModelError("CategoryId", "Category Not Found!");
            }

            subCategory.Name = model.Name;
            subCategory.CategoryId = model.CategoryId;
            subCategory.ModifiedAt = DateTime.Now;

            _subCategoryRepository.Update(subCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id);
            if (subCategory is null)
            {
                _modelState.AddModelError(string.Empty, "Subcategory Not Found!");
                return false;
            }

            subCategory.IsDeleted = true;

            _subCategoryRepository.Delete(subCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
