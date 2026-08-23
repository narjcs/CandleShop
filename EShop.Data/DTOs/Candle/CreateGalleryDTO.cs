using Microsoft.AspNetCore.Http;

namespace EShop.Data.DTOs.Candle
{
    public class CreateGalleryDTO
    {
        public long CandleID { get; set; }
        public IFormFile ImageName { get; set; }
        public int Order { get; set; }
    }
}
