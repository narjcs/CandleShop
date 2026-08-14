using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class ProductVariant : BaseEntity
    {
        public long ProductId { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }
        public Product Product { get; set; }
    }
}
