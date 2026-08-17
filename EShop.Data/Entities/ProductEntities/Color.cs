using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class Color : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<CandleDetail> CandleDetails { get; set; }
    }
}