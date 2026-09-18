using EShop.Application.Extensions;
using EShop.Application.Services.Interfaces;
using EShop.Application.Utils;
using EShop.Data.DTOs.CandleCategory;
using EShop.Data.DTOs.Candles;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.CandleEntities;
using EShop.Data.Entities.OrderEntities;
using EShop.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementations
{
    public class CandleService : ICandleService
    {
        #region CTOR
        private readonly IGenericRepository<Candle> _candleRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Color> _colorRepository;
        private readonly IGenericRepository<Scent> _scentRepository;
        private readonly IGenericRepository<Size> _sizeRepository;
        private readonly IGenericRepository<CandleDetail> _candleDetailRepository;
        private readonly IGenericRepository<SelectedCategory> _selectedCategoryRepository;
        private readonly IGenericRepository<Gallery> _galleryRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;


        public CandleService(IGenericRepository<Candle> candleRepository, IGenericRepository<Category> categoryRepository,
                             IGenericRepository<Color> colorRepository, IGenericRepository<Scent> scentRepository,
                             IGenericRepository<Size> sizeRepository, IGenericRepository<CandleDetail> candleDetailRepository,
                             IGenericRepository<SelectedCategory> selectedCategoryRepository, IGenericRepository<Gallery> galleryRepository,
                             IGenericRepository<OrderDetail> orderDetailRepository)
        {
            _candleRepository = candleRepository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _scentRepository = scentRepository;
            _sizeRepository = sizeRepository;
            _candleDetailRepository = candleDetailRepository;
            _selectedCategoryRepository = selectedCategoryRepository;
            _galleryRepository = galleryRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _candleRepository.DisposeAsync();
            await _categoryRepository.DisposeAsync();
            await _colorRepository.DisposeAsync();
            await _scentRepository.DisposeAsync();
            await _sizeRepository.DisposeAsync();
            await _candleDetailRepository.DisposeAsync();
            await _selectedCategoryRepository.DisposeAsync();
            await _galleryRepository.DisposeAsync();
            await _orderDetailRepository.DisposeAsync();
        }
        #endregion

        #region Candle
        public async Task<CreateCandleResult> CreateCandle(CreateCandleDTO dto)
        {
            var candle = new Candle
            {
                Title = dto.Title,
                Description = dto.Description,
                ShortDescription = dto.ShortDescription,
                IsAvailable = dto.IsAvailable,
            };

            #region Main Image
            var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.MainImage.FileName);
            var result = dto.MainImage.AddImageToServer(mainImageName, PathExtension.CandleImageServer,
                                                       150, 150, PathExtension.CandleImageThumbServer);
            if (result) candle.MainImageName = mainImageName;
            else return CreateCandleResult.SavingMainImageFailed;
            #endregion

            await _candleRepository.AddEntity(candle);
            await _candleRepository.SaveAsync();

            #region Category
            var AddCategoriesResult = await AddCandleSelectedCategories(dto.Categories, candle.Id);
            if (!AddCategoriesResult) return CreateCandleResult.CategoryNotFound;
            #endregion

            #region Galleries
            if (dto.CandleGalleries != null && dto.CandleGalleries.Any())
            {
                var galleryOrder = 2;
                foreach (var item in dto.CandleGalleries)
                {
                    var galleryItem = new Gallery
                    {
                        CandleId = candle.Id,
                        Order = galleryOrder,
                    };

                    //Image
                    var galleryImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(item.FileName);
                    item.AddImageToServer(galleryImageName, PathExtension.CandleGalleryServer,150, 150, PathExtension.CandleGalleryThumbServer);
                    galleryItem.ImageName = galleryImageName;

                    await _galleryRepository.AddEntity(galleryItem);

                    galleryOrder++;
                }
            }
            await _galleryRepository.SaveAsync();
            #endregion

            return CreateCandleResult.Success;
        }
        public async Task<CandleDetailDTO> CandleDetail(long candleId)
        {
            var data = await _candleRepository.GetEntityById(candleId);

            if (data == null || data.IsDeleted)
                return null;

            return new CandleDetailDTO
            {
                Id = data.Id,
                Title = data.Title,
                IsDeleted = data.IsDeleted,
                LastUpdateDate = data.LastUpdateDate,
                CreateDate = data.CreateDate,
                Description = data.Description,
                ShortDescription = data.ShortDescription,
                MainImageName = data.MainImageName,
                IsAvailable = data.IsAvailable,

                CandleDetails = await _candleDetailRepository.GetQuery()
                                .Where(d => d.CandleId == candleId && !d.IsDeleted)
                                .ToListAsync(),
                CandleGalleries = await _galleryRepository.GetQuery()
                                .Where(d => d.CandleId == candleId && !d.IsDeleted)
                                .ToListAsync(),
                SelectedCategories = await _selectedCategoryRepository.GetQuery()
                                .Where(d => d.CandleId == candleId && !d.IsDeleted)
                                .ToListAsync(),

            };
        }
        public async Task<EditCandleResult> EditCandle(EditCandleDTO dto)
        {
            var candle = await _candleRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == dto.CandleId);
            if (candle == null) return EditCandleResult.Error;
            
            candle.Title = dto.Title;
            candle.IsAvailable = dto.IsAvailable;
            candle.ShortDescription = dto.ShortDescription;
            candle.Description = dto.Description;

            #region Category
            await RemoveCandleSelectedCategories(dto.CandleId);
            var AddCategoriesResult = await AddCandleSelectedCategories(dto.Categories , dto.CandleId);
            if (!AddCategoriesResult) return EditCandleResult.CategoryNotFound;
            #endregion

            #region Main Image
            if (dto.MainImage != null)
            {
                var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.MainImage.FileName);
                var result = dto.MainImage.AddImageToServer(mainImageName, PathExtension.CandleImageServer,
                                                           150, 150, PathExtension.CandleImageThumbServer,
                                                           candle.MainImageName);
                if (result) candle.MainImageName = mainImageName;
                else return EditCandleResult.ImageNotSaved;
            }
            #endregion

            _candleRepository.EditEntity(candle);
            await _candleRepository.SaveAsync();
            return EditCandleResult.Success;
        }
        public async Task<FilterCandleDTO> FilterCandle(FilterCandleDTO filter)
        {
            #region Query
            var query = _candleRepository.GetQuery().Include(d => d.CandleDetails)
                .Include(d => d.SelectedCategories).ThenInclude(d => d.Category)
                .AsQueryable();
            #endregion

            #region Switch
            switch (filter.CandleOrder)
            {
                case FilterCandleOrder.Newest:
                    query = query.OrderByDescending(d => d.CreateDate);
                    break;
                case FilterCandleOrder.Oldest:
                    query = query.OrderBy(d => d.CreateDate);
                    break;
                case FilterCandleOrder.MostExpensive:
                    query = query.OrderByDescending(d => d.Price);
                    break;
                case FilterCandleOrder.Cheapest:
                    query = query.OrderBy(d => d.Price);
                    break;
                default:
                    query = query.OrderByDescending(d => d.CreateDate);
                    break;
            }
            switch (filter.CandleStatus)
            {
                case FilterCandleStatus.All:
                    break;
                case FilterCandleStatus.Available:
                    query = query.Where(d => d.IsAvailable);
                    break;
                case FilterCandleStatus.NotAvailable:
                    query = query.Where(d => !d.IsAvailable);
                    break;
                case FilterCandleStatus.HasStockCount:
                    query = query.Where(d => d.CandleDetails.Any(v => v.StockCount > 0));
                    break;
                case FilterCandleStatus.HasZeroStockCount:
                    query = query.Where(d => !d.CandleDetails.Any(v => v.StockCount > 0));
                    break;
                default:
                    break;
            }
            #endregion

            #region Filter

            #region Title
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                var searchTitle = filter.Title.Trim();
                query = query.Where(p => EF.Functions.Like(p.Title, $"%{searchTitle}%"));
            }
            #endregion

            #region Price
            if (filter.StartPrice != null)
            {
                query = query.Where(d => d.Price >= filter.StartPrice);
            }
            if (filter.EndPrice != null)
            {
                query = query.Where(d => d.Price <= filter.EndPrice);
            }
            if (await query.AnyAsync())
            {
                filter.MostPrice = await query.MaxAsync(d => d.Price);
                filter.LeastPrice = await query.MinAsync(d => d.Price);
            }
            #endregion

            #region Category
            if (filter.CategoryId is > 0)
            {
                query = query.Where(d => d.SelectedCategories.Any(s => s.CategoryId == filter.CategoryId));
            }
            #endregion

            #region Color
            if (filter.ColorId is > 0)
            {
                query = query.Where(d => d.CandleDetails.Any(s => s.ColorId == filter.ColorId));
            }
            #endregion

            #region Scent
            if (filter.ScentId is > 0)
            {
                query = query.Where(d => d.CandleDetails.Any(s => s.ScentId == filter.ScentId));
            }
            #endregion

            #region Size
            if (filter.SizeId is > 0)
            {
                query = query.Where(d => d.CandleDetails.Any(s => s.SizeId == filter.SizeId));
            }
            #endregion

            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetData(allEntities).SetPaging(pager);
        }
        public async Task<EditCandleDTO> GetEditCandle(long candleId)
        {
            var data = await _candleRepository.GetEntityById(candleId);
            var model = new EditCandleDTO
            {
                CandleId = candleId,
                Title = data.Title,
                IsAvailable = data.IsAvailable,
                ShortDescription = data.ShortDescription,
                Description = data.Description,
                Categories = await _selectedCategoryRepository.GetQuery().Where(d => d.CandleId == candleId)
                            .Select(d => d.CategoryId).ToListAsync(),
            };
            return model;
        }
        public async Task<bool> DeleteCandle(long candleId)
        {
            #region Check Order
            var candleOrdered = await _orderDetailRepository.GetQuery().AnyAsync(d => d.CandleId == candleId);
            if (candleOrdered) return false;
            #endregion

            #region Categories
            var categories = await _selectedCategoryRepository.GetQuery().Where(d => d.CandleId == candleId).ToListAsync();
            _selectedCategoryRepository.DeletePermanentEntities(categories);
            await _selectedCategoryRepository.SaveAsync();
            #endregion

            #region Galleries
            var galleries = await _galleryRepository.GetQuery().Where(d => d.CandleId == candleId).ToListAsync();
            if (galleries.Any())
            {
                foreach (var item in galleries)
                {
                    item.ImageName.DeleteImage(PathExtension.CandleGalleryImage, PathExtension.CandleGalleryThumb);
                }
                _galleryRepository.DeletePermanentEntities(galleries);
                await _galleryRepository.SaveAsync();
            }
            #endregion

            var candle = await _candleRepository.GetEntityById(candleId);
            _candleRepository.DeleteEntity(candle);
            await _candleRepository.SaveAsync();
            return true;
        }
        #endregion

        #region Category
        public async Task<bool> CreateCategory(CreateCategoryDTO dto)
        {
            #region Check Url
            var urlInUse = await _categoryRepository.GetQuery().AnyAsync(c => c.Url ==  dto.Url);
            if (urlInUse) return false;
            #endregion

            var category = new Category
            {
                Title = dto.Title,
                Url = dto.Url,
                IsActive = true,
                Order = dto.Order
            };
            await _categoryRepository.AddEntity(category);
            await _categoryRepository.SaveAsync();
            return true;
        }
        public async Task<bool> EditCategory(EditCategoryDTO dto)
        {
            #region Check Url
            var urlInUse = await _categoryRepository.GetQuery().AnyAsync(c => c.Url == dto.Url);
            if (urlInUse) return false;
            #endregion

            var data = await _categoryRepository.GetEntityById(dto.CategoryId);
            data.Title = dto.Title;
            data.Url = dto.Url;
            data.IsActive = dto.IsActive;
            data.Order = dto.Order;

            _categoryRepository.EditEntity(data);
            await _categoryRepository.SaveAsync();
            return true;
        }
        public async Task<FilterCategoryDTO> FilterCategory(FilterCategoryDTO filter)
        {
            #region Query
            var query = _categoryRepository.GetQuery()
                .OrderByDescending(d => d.CreateDate).AsQueryable();
            #endregion

            #region Switch
            switch (filter.CategoryStatus)
            {
                case FilterCategoryStatus.All:
                    break;
                case FilterCategoryStatus.Active:
                    query = query.Where(d => d.IsActive);
                    break;
                case FilterCategoryStatus.DeActive:
                    query = query.Where(d => !d.IsActive);
                    break;
                default:
                    break;
            }
            #endregion

            #region Filter
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                var searchTitle = filter.Title.Trim();
                query = query.Where(p => EF.Functions.Like(p.Title, $"%{searchTitle}%"));
            }
            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetData(allEntities).SetPaging(pager);
        }
        public async Task<EditCategoryDTO> GetEditCategory(long categortId)
        {
            var data = await _categoryRepository.GetEntityById(categortId);
            return new EditCategoryDTO
            {
                Title = data.Title,
                Url = data.Url,
                IsActive = data.IsActive,
                Order = data.Order
            };
        }
        public async Task<bool> DeleteCategory(long categoryId)
        {
            #region Check Category
            var categoryInUse = await _selectedCategoryRepository.GetQuery().AnyAsync(c => c.CategoryId == categoryId);
            if (categoryInUse) return false;
            #endregion

            var data = await _categoryRepository.GetEntityById(categoryId);
            _categoryRepository.DeleteEntity(data);
            await _categoryRepository.SaveAsync();
            return true;
        }
        public async Task<bool> AddCandleSelectedCategories(List<long> selectedCategories, long candleId)
        {
            foreach (var category in selectedCategories)
            {
                var selectedCategory = await _categoryRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == category);
                if (selectedCategory == null) return false;

                var selected = new SelectedCategory
                {
                    Candle = await _candleRepository.GetEntityById(candleId),
                    Category = selectedCategory,
                    CandleId = candleId,
                    CategoryId = category
                };
                await _selectedCategoryRepository.AddEntity(selected);
            }
            await _selectedCategoryRepository.SaveAsync();
            return true;
        }
        public async Task RemoveCandleSelectedCategories(long candleId)
        {
            var selectedCategories = await _selectedCategoryRepository.GetQuery().Where(d => d.CandleId == candleId).ToListAsync();
            _selectedCategoryRepository.DeletePermanentEntities(selectedCategories);
            await _selectedCategoryRepository.SaveAsync();
        }
        #endregion

        #region CandleDetail
        public Task CreateCandleDetail(CreateCandleDetailDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<EditCandleDetailDTO> GetEditCandleDetail(long detailId)
        {
            throw new NotImplementedException();
        }
        public Task EditCandleDetail(EditCandleDetailDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteCandleDetail(long detailId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Color
        public async Task<FilterColorDTO> FilterColor(FilterColorDTO filter)
        {
            var query = _colorRepository.GetQuery().OrderByDescending(d => d.CreateDate).AsQueryable();

            #region Filter
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                var searchTitle = filter.Title.Trim();
                query = query.Where(p => EF.Functions.Like(p.Title, $"%{searchTitle}%"));
            }
            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetData(allEntities).SetPaging(pager);
        }
        public async Task<List<Color>> GetAllCandleColors()
        {
            return await _colorRepository.GetQuery().ToListAsync();
        }
        public async Task CreateColor(CreateColorDTO dto)
        {
            var color = new Color
            {
                Title = dto.Title,
                ColorCode = dto.ColorCode
            };
            await _colorRepository.AddEntity(color);
            await _colorRepository.SaveAsync();
        }
        public async Task<EditColorDTO> GetEditColor(long colorId)
        {
            var data = await _colorRepository.GetEntityById(colorId);
            return new EditColorDTO
            {
                Title = data.Title,
                ColorCode = data.ColorCode,
                ColorId = data.Id
            };
        }
        public async Task EditColor(EditColorDTO dto)
        {
            var data = await _colorRepository.GetEntityById(dto.ColorId);
            data.Title = dto.Title;
            data.ColorCode = dto.ColorCode;
            _colorRepository.EditEntity(data);
            await _colorRepository.SaveAsync();
        }
        public async Task<bool> DeleteColor(long colorId)
        {
            var InUse = await _candleDetailRepository.GetQuery().AnyAsync(c => c.ColorId == colorId);
            if (InUse) return false;

            var data  = await _colorRepository.GetEntityById(colorId);
            _colorRepository.DeleteEntity(data);
            await _colorRepository.SaveAsync();
            return true;
        }
        #endregion

        #region Size
        public Task<FilterSizeDTO> FilterSize(FilterSizeDTO filter)
        {
            throw new NotImplementedException();
        }
        public Task CreateSize(CreateSizeDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<EditSizeDTO> GetEditSize(long sizeId)
        {
            throw new NotImplementedException();
        }
        public Task EditSize(EditSizeDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteSize(long sizeId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Scent
        public Task<FilterScentDTO> FilterScent(FilterScentDTO filter)
        {
            throw new NotImplementedException();
        }
        public Task CreateScent(CreateScentDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<EditScentDTO> GetEditScent(long scentId)
        {
            throw new NotImplementedException();
        }
        public Task EditScent(EditScentDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteScent(long scentId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Gallery
        public async Task CreateGallery(CreateGalleryDTO dto)
        {
            var gallery = new Gallery
            {
                Order = dto.Order,
                CandleId = dto.CandleID
            };

            #region Main Image
            var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.ImageName.FileName);
            dto.ImageName.AddImageToServer(mainImageName, PathExtension.CandleGalleryServer,
                                                       150, 150, PathExtension.CandleGalleryThumbServer);
            gallery.ImageName = mainImageName;
            #endregion

            await _galleryRepository.AddEntity(gallery);
            await _galleryRepository.SaveAsync();
        }
        public async Task<EditGalleryDTO> GetEditGallery(long galleryId)
        {
            var data = await _galleryRepository.GetEntityById(galleryId);
            return new EditGalleryDTO
            {
                Order = data.Order,
                GalleryID = data.Id
            };
        }
        public async Task EditGallery(EditGalleryDTO dto)
        {
            var data = await _galleryRepository.GetEntityById(dto.GalleryID);
            data.Order = dto.Order;
            _galleryRepository.EditEntity(data);
            await _galleryRepository.SaveAsync();
        }
        public async Task<bool> DeleteGallery(long galleryId)
        {
            var data = await _galleryRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == galleryId);
            if (data == null) return false;

            data.ImageName.DeleteImage(PathExtension.CandleGalleryImage, PathExtension.CandleGalleryThumb);
            _galleryRepository.DeletePermanent(data);
            await _galleryRepository.SaveAsync();
            return true;
        }
        #endregion
    }
}