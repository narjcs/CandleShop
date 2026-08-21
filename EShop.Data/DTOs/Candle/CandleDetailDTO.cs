using EShop.Data.Entities.Candle;

namespace EShop.Data.DTOs.Candle
{
    public class CandleDetailDTO
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string MainImageName { get; set; }

        public List<SelectedCategory> SelectedCategories { get; set; }
        public List<CandleDetail> CandleDetails { get; set; }
        public List<Gallery> CandleGalleries { get; set; }
    }
}
