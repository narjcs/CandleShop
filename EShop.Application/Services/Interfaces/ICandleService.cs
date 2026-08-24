using EShop.Data.DTOs.Candle;
using EShop.Data.DTOs.CandleCategory;

namespace EShop.Application.Services.Interfaces
{
    public interface ICandleService : IAsyncDisposable
    {
        #region Candle
        Task<FilterCandleDTO> FilterCandle(FilterCandleDTO filter);
        Task<CandleDetailDTO> CandleDetail(long candleId);
        Task<CreateCandleResult> CreateCandle(CreateCandleDTO dto);
        Task<EditCandleDTO> GetEditCandle(long candleId); // First we fill an EditProductDTO then we send it to Admin
        Task<EditCandleResult> EditCandle(EditCandleDTO dto); //Admin update or change any essential part
        Task<bool> DeleteCandle(long candleId);
        #endregion

        #region Categories
        Task AddCandleSelectedCategories(List<long> selectedCategories, long candleId);
        Task RemoveCandleSelectedCategories(long candleId);
        Task CreateCategory(CreateCategoryDTO dto);
        Task EditCategory(EditCategoryDTO dto);
        Task<EditCategoryDTO> GetEditCategory(long categortId);
        Task<FilterCategoryDTO> FilterCategory(FilterCategoryDTO filter);
        Task<bool> DeleteCategory(long categoryId);
        #endregion
        
        #region CandleDetail
        Task CreateCandleDetail(CreateCandleDetailDTO dto);
        Task<EditCandleDetailDTO> GetEditCandleDetail(long detailId);
        Task EditCandleDetail(EditCandleDetailDTO dto);
        Task<bool> DeleteCandleDetail(long detailId);
        #endregion

        #region Color
        Task<FilterColorDTO> FilterColor(FilterColorDTO filter);
        Task CreateColor(CreateColorDTO dto);
        Task<EditColorDTO> GetEditColor(long colorId);
        Task EditColor(EditColorDTO dto);
        Task<bool> DeleteColor(long colorId);
        #endregion

        #region Size
        Task<FilterSizeDTO> FilterSize(FilterSizeDTO filter);
        Task CreateSize(CreateSizeDTO dto);
        Task<EditSizeDTO> GetEditSize(long sizeId);
        Task EditSize(EditSizeDTO dto);
        Task<bool> DeleteSize(long sizeId);
        #endregion

        #region Scent
        Task<FilterScentDTO> FilterScent(FilterScentDTO filter);
        Task CreateScent(CreateScentDTO dto);
        Task<EditScentDTO> GetEditScent(long scentId);
        Task EditScent(EditScentDTO dto);
        Task<bool> DeleteScent(long scentId);
        #endregion

        #region Gallery
        Task CreateGallery(CreateGalleryDTO dto);
        Task<EditGalleryDTO> GetEditGallery(long galleryId);
        Task EditGallery(EditGalleryDTO dto);
        Task<bool> DeleteGallery(long galleryId);

        #endregion
    }
}