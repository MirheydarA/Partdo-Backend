using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Slider;
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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private ModelStateDictionary _modelState;
        public SliderService(ISliderRepository sliderRepository,
                             IUnitOfWork unitOfWork,
                             IActionContextAccessor actionContextAccessor,
                             IFileService fileService)

        {
            _sliderRepository = sliderRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<SliderIndexVM> GetAllAsync(bool showDeleted = false)
        {
            var model = new SliderIndexVM();
            if (true)
            {
                model.Sliders = await _sliderRepository.GetAllAsync();
            }
            //else
            //{
            //    model.Sliders = await _sliderRepository.GetActiveAsync();

            //}
            return model;
        }

        public async Task<bool> CreateAsync(SliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.PhotoName))
            {
                _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.PhotoName, 3000))
            {
                _modelState.AddModelError("Photoname", "Sekilin olcusu 3000kb dan boyukdur");
                return false;
            }

            var slider = new Slider
            {
                Title = model.Title,
                Description = model.Description,
                PhotoName = _fileService.Upload(model.PhotoName),
                CreatedAt = DateTime.Now
            };

            await _sliderRepository.CreateAsync(slider);
            await _unitOfWork.CommitAsync();

            return true;

        }
        public async Task<SliderUpdateVM?> GetUpdateAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);

            if (slider is null) return null;

            var model = new SliderUpdateVM
            {
                Title = slider.Title,
                Description = slider.Description,
                PhotoPath = slider.PhotoName
            };

            return model;
        }

        public async Task<bool> PostUpdateAsync(int id, SliderUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider is null)
            {
                _modelState.AddModelError(string.Empty, "Slider not found");
                return false;
            }

            if (model.PhotoName is not null)
            {
                if (!_fileService.IsImage(model.PhotoName))
                {
                    _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                    return false;
                }

                if (!_fileService.IsBiggerThanSize(model.PhotoName, 3000))
                {
                    _modelState.AddModelError("Photoname", "Sekilin olcusu 3000kb dan boyukdur");
                    return false;
                }
                //kohne sekili silmek
                _fileService.Delete(slider.PhotoName);
                slider.PhotoName = _fileService.Upload(model.PhotoName);
            }


            slider.Title = model.Title;
            slider.Description = model.Description;
            slider.ModifiedAt = DateTime.Now;

            _sliderRepository.Update(slider);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider is null)
            {
                _modelState.AddModelError(string.Empty, "Slider Not Found");
                return false;
            }

            slider.IsDeleted = true;
            _sliderRepository.Delete(slider);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
