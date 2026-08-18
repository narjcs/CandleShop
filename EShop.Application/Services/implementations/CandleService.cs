using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.Candle;

namespace EShop.Application.Services.implementations
{
    public class CandleService : ICandleService
    {
        public Task<CandleDetailDTO> CandleDetail(long candleId)
        {
            throw new NotImplementedException();
        }

        public Task<CreateCandleDTO> CreateCandle(CreateCandleDTO dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FilterCandleDTO> FilterCandle(FilterCandleDTO filter)
        {
            throw new NotImplementedException();
        }
    }
}