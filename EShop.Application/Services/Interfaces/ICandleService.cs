using EShop.Data.DTOs.Candle;
using EShop.Data.DTOs.CandleCategory;

namespace EShop.Application.Services.Interfaces
{
    public interface ICandleService : IAsyncDisposable
    {
        #region Candle
        Task<FilterCandleDTO> FilterCandle(FilterCandleDTO filter);
        Task<CandleDetailDTO> CandleDetail(long candleId);
        Task<CreateCandleDTO> CreateCandle(CreateCandleDTO dto);
        Task<EditCandleDTO> GetEditCandle(long candleId); // First we fill an EditProductDTO then we send it to Admin
        Task EditProduct(EditCandleDTO dto); //Admin update or change any essential part
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
    }
}