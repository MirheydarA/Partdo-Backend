using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Product;
using Common.Entities;
using DataAccess.Migrations;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IFileService _fileService;
        private readonly IProductPhotoRepository _productPhotoRepository;

        public ProductService(IProductRepository productRepository,
                              IActionContextAccessor actionContextAccessor,
                              IUnitOfWork unitOfWork,
                              ISubCategoryRepository subCategoryRepository,
                              IFileService fileService,
                              IProductPhotoRepository productPhotoRepository)
        {
            _productRepository = productRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _subCategoryRepository = subCategoryRepository;
            _fileService = fileService;
            _productPhotoRepository = productPhotoRepository;
        }

        public async Task<ProductIndexVM> IndexGetAllAsync(ProductIndexVM model)
        {
            
            if (model.IsChecked == true)
            {
                model.Products = await _productRepository.GetAllProductsWithIncludedAsync();
                return model;
            }

            model.Products = await _productRepository.GetProductsWithIncludedAsync();
            return model;
        }

        public async Task<ProductCreateVM> GetCreateAsync()
        {
            //var products = await _productRepository.GetProductsWithIncludedAsync();
            var model = new ProductCreateVM();
            var listSubCategory = await _subCategoryRepository.GetAllAsync();

            model.SubCategories = listSubCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            return model;
        }

        public async Task<bool> PostCreateAsync(ProductCreateVM model)
        {
            var listSubCategory = await _subCategoryRepository.GetSubCategoriesWithCategory();

            model.SubCategories = listSubCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("Photos", "File is not in image format");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.MainPhoto, 3000))
            {
                _modelState.AddModelError("Photos", "Image is bigger than 3000kb");
                return false;
            }


            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("Photos", "File is not in image format");
                    return false;
                }

                if (!_fileService.IsBiggerThanSize(photo, 3000))
                {
                    _modelState.AddModelError("Photos", "Image is bigger than 3000kb");
                    return false;
                }
            }

            if (!await _productRepository.GetProductByNameAsync(model.Name))
            {
                _modelState.AddModelError("Name", "This name already used");
                return false;
            }

            var product = new Product
            {
                Name = model.Name,
                MainPhoto = _fileService.Upload(model.MainPhoto),
                Description = model.Description,
                ShortDesc = model.ShortDesc,
                StockType = model.StockType,
                StockQuantity = model.StockQuantity,
                Status = model.Status,
                Price = model.Price,
                NewPrice = model.NewPrice,
                SubCategoryId = model.SubCategroyId,
                IsSpecial = model.IsSpecial,
                IsMain = model.IsMain,
                CreatedAt = DateTime.Now
            };

            foreach (var photo in model.Photos)
            {
                var productPhotos = new ProductPhoto
                {
                    Name = _fileService.Upload(photo),
                    Product = product
                };

                await _productPhotoRepository.CreateAsync(productPhotos); 
            }


            if (product.StockQuantity < 0)
            {
                _modelState.AddModelError("StockQuantity", "Stock quantity must be bigger than 0");
                return false;
            }

            if (product.Price < 0)
            {
                _modelState.AddModelError("Price", "Price must be bigger than 0");
                return false;
            }
            if (product.NewPrice < 0)
            {
                _modelState.AddModelError("OldPrice", " Price must be bigger than 0");
                return false;
            }


            await _productRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<ProductUpdateVM?> GetUpdateAsync(int id)
        {
            var product = await _productRepository.GetProductWithIncludeById(id);
            if (product == null) return null;

            var listSubCategory = await _subCategoryRepository.GetSubCategoriesWithCategory();

            List<ProductPhoto> listProductPhotos = new List<ProductPhoto>();
            foreach (var photo in product.Photos)
            {
                listProductPhotos.Add(photo);
            }

            var model = new ProductUpdateVM
            {
                SubCategories = listSubCategory.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList(),

                Name = product.Name,
                Description = product.Description,
                ShortDesc = product.ShortDesc,
                StockType = product.StockType,
                StockQuantity = product.StockQuantity,
                Status = product.Status,
                Price = product.Price,
                NewPrice = product.NewPrice,
                SubCategoryId = product.SubCategoryId,
                MainPhotoPath = product.MainPhoto,
                PhotoNames = product.Photos.ToList(),
                IsSpecial = product.IsSpecial,
                IsMain = product.IsMain,
            };

            return model;
        }
        public async Task<bool> PostUpdateAsync(int id, ProductUpdateVM model)
        {
            var listSubCategory = await _subCategoryRepository.GetSubCategoriesWithCategory();
            var subCategories = listSubCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            if (!_modelState.IsValid) return false;

            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                _modelState.AddModelError(string.Empty, "Product Not Found!");
            }

            var subCategory = await _subCategoryRepository.GetByIdAsync(model.SubCategoryId);
            if (subCategory is null)
            {
                _modelState.AddModelError("SubCategoryId", "SubCategory Not Found!");
            }

            if (model.MainPhoto is not null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                    return false;
                }

                if (!_fileService.IsBiggerThanSize(model.MainPhoto, 3000))
                {
                    _modelState.AddModelError("Photoname", "Sekilin olcusu 3000kb dan boyukdur");
                    return false;
                }
                //kohne sekili silmek
                _fileService.Delete(product.MainPhoto);
                product.MainPhoto = _fileService.Upload(model.MainPhoto);
            }

            if (model.Photos is not null)
            {
                List<ProductPhoto> listProductPhotos = new List<ProductPhoto>();

                foreach (var photo in listProductPhotos)
                {
                    _fileService.Delete(photo.Name);
                }


                foreach (var photo in model.Photos)
                {
                    if (!_fileService.IsImage(photo))
                    {
                        _modelState.AddModelError("Photos", "File is not in image format");
                        return false;
                    }

                    if (!_fileService.IsBiggerThanSize(photo, 3000))
                    {
                        _modelState.AddModelError("Photos", "Image is bigger than 3000kb");
                        return false;
                    }

                    var productPhotos = new ProductPhoto();

                    _fileService.Delete(productPhotos.Name);
                    productPhotos.Name = _fileService.Upload(photo);
                    productPhotos.Product = product;

                    listProductPhotos.Add(productPhotos);
                }


                product.Photos = new ReadOnlyCollection<ProductPhoto>(listProductPhotos);
            }


            product.Name = model.Name;
            //product.MainPhoto = model.MainPhotoPath;
            product.Description = model.Description;
            product.ShortDesc = model.ShortDesc;
            product.StockType = model.StockType;
            product.StockQuantity = model.StockQuantity;
            product.Price = model.Price;
            product.NewPrice = model.NewPrice;
            product.SubCategoryId = model.SubCategoryId;
            product.IsSpecial = model.IsSpecial;
            product.IsMain = model.IsMain; 
            product.ModifiedAt = DateTime.Now;

            //foreach (var photo in model.Photos)
            //{
            //    var productPhotos = new ProductPhoto
            //    {
            //        Name = _fileService.Upload(photo),
            //        Product = product
            //    };

            //    _productPhotoRepository.Update(productPhotos);
            //}
            _productRepository.Update(product);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                _modelState.AddModelError(string.Empty, "Product Not Found!");
                return false;
            }

            product.IsDeleted = true;

            _productRepository.Delete(product);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<ProductDetailsVM>? DetailsAsync(int id)
        {
            var product = await _productRepository.GetProductWithIncludeById(id);
            if (product is null) return null;

            var selectedProduct = new ProductDetailsVM()
            {
                Name = product.Name,
                Description = product.Description,
                ShortDesc = product.ShortDesc,
                StockType = product.StockType,
                StockQuantity = product.StockQuantity,
                Status = product.Status,
                Price = product.Price,
                NewPrice = product.NewPrice,
                SubCategory = product.SubCategory,
                CreatedAt = product.CreatedAt,
                ModifiedAt = product.ModifiedAt,
                Photos = product.Photos,
                MainPhoto = product.MainPhoto,
                IsSpecial = product.IsSpecial,
                IsMain = product.IsMain,
            };

            return selectedProduct;
        }
    }
}
