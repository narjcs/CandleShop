using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.Candle;
using EShop.Data.DTOs.CandleCategory;
using EShop.Data.Entities.Candle;
using EShop.Data.Repository;

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

        public CandleService(IGenericRepository<Candle> candleRepository, IGenericRepository<Category> categoryRepository,
                             IGenericRepository<Color> colorRepository, IGenericRepository<Scent> scentRepository,
                             IGenericRepository<Size> sizeRepository, IGenericRepository<CandleDetail> candleDetailRepository,
                             IGenericRepository<SelectedCategory> selectedCategoryRepository, IGenericRepository<Gallery> galleryRepository)
        {
            _candleRepository = candleRepository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _scentRepository = scentRepository;
            _sizeRepository = sizeRepository;
            _candleDetailRepository = candleDetailRepository;
            _selectedCategoryRepository = selectedCategoryRepository;
            _galleryRepository = galleryRepository;
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
        }
        #endregion

        #region Candle
        public Task<CreateCandleDTO> CreateCandle(CreateCandleDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<CandleDetailDTO> CandleDetail(long candleId)
        {
            throw new NotImplementedException();
        }
        public Task EditProduct(EditCandleDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<FilterCandleDTO> FilterCandle(FilterCandleDTO filter)
        {
            throw new NotImplementedException();
        }
        public Task<EditCandleDTO> GetEditCandle(long candleId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteCandle(long candleId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Category
        public Task CreateCategory(CreateCategoryDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task EditCategory(EditCategoryDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<FilterCategoryDTO> FilterCategory(FilterCategoryDTO filter)
        {
            throw new NotImplementedException();
        }
        public Task<EditCategoryDTO> GetEditCategory(long categortId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteCategory(long categoryId)
        {
            throw new NotImplementedException();
        }
        public Task AddCandleSelectedCategories(List<long> selectedCategories, long candleId)
        {
            throw new NotImplementedException();
        }
        public Task RemoveCandleSelectedCategories(long candleId)
        {
            throw new NotImplementedException();
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
        public Task<FilterColorDTO> FilterColor(FilterColorDTO filter)
        {
            throw new NotImplementedException();
        }
        public Task CreateColor(CreateColorDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<EditColorDTO> GetEditColor(long colorId)
        {
            throw new NotImplementedException();
        }
        public Task EditColor(EditColorDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteColor(long colorId)
        {
            throw new NotImplementedException();
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
        public Task CreateGallery(CreateGalleryDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<EditGalleryDTO> GetEditGallery(long galleryId)
        {
            throw new NotImplementedException();
        }
        public Task EditGallery(EditGalleryDTO dto)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteGallery(long galleryId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}