using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class Scent : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<CandleDetail> CandleDetails { get; set; }
    }
}