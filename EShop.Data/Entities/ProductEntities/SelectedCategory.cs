namespace EShop.Data.Entities.ProductEntities
{
    public class SelectedCategory
    {
        public long CandleId { get; set; }
        public long CategoryId { get; set; }
        public Candle Candle { get; set; }
        public Category Category { get; set; }
    }
}