namespace EShop.Data.Entities.ProductEntities
{
    public class ProductFeature
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public Product Product { get; set; }
    }
}
