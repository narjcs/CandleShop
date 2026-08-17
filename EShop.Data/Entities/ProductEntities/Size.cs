namespace EShop.Data.Entities.ProductEntities
{
    public class Size
    {
        public string Title { get; set; }
        public ICollection<CandleDetail> CandleDetails { get; set; }
    }
}