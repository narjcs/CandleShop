using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.Candle
{
    public class Scent : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<CandleDetail> CandleDetails { get; set; }
    }
}