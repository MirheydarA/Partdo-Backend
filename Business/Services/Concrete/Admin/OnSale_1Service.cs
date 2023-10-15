using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Category;
using Business.ViewModels.Admin.OnSale_1;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class OnSale_1Service : IOnSale_1Service
    {
        private ModelStateDictionary _modelState;
        private readonly IOnSale_1Repository _onSale_1Repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public OnSale_1Service(IOnSale_1Repository onSale_1Repository,
                              IActionContextAccessor actionContextAccessor,
                              IUnitOfWork unitOfWork,
                              IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _onSale_1Repository = onSale_1Repository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<OnSale_1IndexVM> IndexGetAllAsync()
        {
            var model = new OnSale_1IndexVM();
            model.OnSale_1s = await _onSale_1Repository.GetAllAsync();
            return model;
        }
        public async Task<bool> PostCreateAsync(OnSale_1CreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.PhotoName))
            {
                _modelState.AddModelError("PhotoName", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.PhotoName, 3000))
            {
                _modelState.AddModelError("PhotoName", "Sekilin olcusu 3000kb dan boyukdur");
                return false;
            }

            var onSale_1 = new OnSale_1Component
            {
                Title = model.Title,
                Description = model.Description,
                Photo = _fileService.Upload(model.PhotoName),
                CreatedAt = DateTime.Now,
            };

            await _onSale_1Repository.CreateAsync(onSale_1);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<OnSale_1UpdateVM?> GetUpdateAsync(int id)
        {
            var onSale_1 = await _onSale_1Repository.GetByIdAsync(id);

            if (onSale_1 is null) return null;

            var model = new OnSale_1UpdateVM
            {
                Title = onSale_1.Title,
                Description = onSale_1.Description,
                PhotoPath = onSale_1.Photo
            };
            return model;
        }
        public async Task<bool> PostUpdateAsync(int id, OnSale_1UpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var onSale_1 = await _onSale_1Repository.GetByIdAsync(id);

            if (onSale_1 is null)
            {
                _modelState.AddModelError(string.Empty, "On Sale not found");
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
                _fileService.Delete(onSale_1.Photo);
                onSale_1.Photo = _fileService.Upload(model.Photo);
            }

            onSale_1.Title = model.Title;
            onSale_1.Description = model.Description;
            onSale_1.ModifiedAt = DateTime.Now;

            _onSale_1Repository.Update(onSale_1);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var onSale_1 = await _onSale_1Repository.GetByIdAsync(id);

            if (onSale_1 is null)
            {
                _modelState.AddModelError(string.Empty, "On Sale not found");
                return false;
            }

            onSale_1.IsDeleted = true;

            _onSale_1Repository.Delete(onSale_1);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
