using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class ProductCategory : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}
