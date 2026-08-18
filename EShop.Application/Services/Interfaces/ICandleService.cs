using EShop.Data.DTOs.Candle;

namespace EShop.Application.Services.Interfaces
{
    public interface ICandleService : IAsyncDisposable
    {
        #region Candle
        Task<FilterCandleDTO> FilterCandle(FilterCandleDTO filter);
        Task<CandleDetailDTO> CandleDetail(long candleId);
        Task<CreateCandleDTO> CreateCandle(CreateCandleDTO dto);
        #endregion
    }
}