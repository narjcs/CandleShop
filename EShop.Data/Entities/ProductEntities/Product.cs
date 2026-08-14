using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class Product : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string MainImageName { get; set; }
        public long CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        #endregion

        #region Relations
        public ICollection<ProductVariant> ProductVariants { get; set; }
        public ICollection<ProductGallery> ProductGalleries { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        #endregion
    }
}
