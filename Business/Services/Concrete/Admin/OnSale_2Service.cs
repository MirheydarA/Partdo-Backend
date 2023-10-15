using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.OnSale_2;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
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
    public class OnSale_2Service : IOnSale_2Service
    {
        private ModelStateDictionary _modelState;
        private readonly IOnSale_2Repository _onSale_2Repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public OnSale_2Service(IOnSale_2Repository onSale_2Repository,
                              IActionContextAccessor actionContextAccessor,
                              IUnitOfWork unitOfWork,
                              IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _onSale_2Repository = onSale_2Repository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<OnSale_2IndexVM> IndexGetAllAsync()
        {
            var model = new OnSale_2IndexVM();
            model.OnSale_2s = await _onSale_2Repository.GetAllAsync();
            return model;
        }
        public async Task<bool> PostCreateAsync(OnSale_2CreateVM model)
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

            var onSale_2 = new OnSale_2Component
            {
                Title = model.Title,
                Description = model.Description,
                Photo = _fileService.Upload(model.PhotoName),
                CreatedAt = DateTime.Now,
            };

            await _onSale_2Repository.CreateAsync(onSale_2);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<OnSale_2UpdateVM?> GetUpdateAsync(int id)
        {
            var onSale_2 = await _onSale_2Repository.GetByIdAsync(id);

            if (onSale_2 is null) return null;

            var model = new OnSale_2UpdateVM
            {
                Title = onSale_2.Title,
                Description = onSale_2.Description,
                PhotoPath = onSale_2.Photo
            };
            return model;
        }
        public async Task<bool> PostUpdateAsync(int id, OnSale_2UpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var onSale_2 = await _onSale_2Repository.GetByIdAsync(id);

            if (onSale_2 is null)
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
                _fileService.Delete(onSale_2.Photo);
                onSale_2.Photo = _fileService.Upload(model.Photo);
            }

            onSale_2.Title = model.Title;
            onSale_2.Description = model.Description;
            onSale_2.ModifiedAt = DateTime.Now;

            _onSale_2Repository.Update(onSale_2);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var onSale_2 = await _onSale_2Repository.GetByIdAsync(id);

            if (onSale_2 is null)
            {
                _modelState.AddModelError(string.Empty, "On Sale not found");
                return false;
            }

            onSale_2.IsDeleted = true;

            _onSale_2Repository.Delete(onSale_2);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
