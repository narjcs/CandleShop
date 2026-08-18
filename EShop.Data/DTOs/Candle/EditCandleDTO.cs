using Microsoft.AspNetCore.Http;

namespace EShop.Data.DTOs.Candle
{
    public class EditCandleDTO
    {
        public long CandleId { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public IFormFile MainImage { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<long> Categories { get; set; }
    }
    public enum EditCandleResult
    {
        Success,
        Error,
        FileNotImage,
        CategoryNotFound
    }
}
