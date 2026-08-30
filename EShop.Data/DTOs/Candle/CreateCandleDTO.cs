using Microsoft.AspNetCore.Http;

namespace EShop.Data.DTOs.Candle

{
    public class CreateCandleDTO
    {
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public IFormFile MainImage { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<long> Categories { get; set; }
        public List<IFormFile>? CandleGalleries { get; set; }
    }

    public enum CreateCandleResult
    {
        Success,
        Error,
        SavingMainImageFailed,
        CategoryNotFound
    }
}