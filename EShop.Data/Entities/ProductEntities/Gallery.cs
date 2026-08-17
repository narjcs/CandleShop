namespace EShop.Data.Entities.ProductEntities
{
    public class Gallery
    {
        public long CandleId { get; set; }
        public string ImageName { get; set; }
        public int Order { get; set; }
        public Candle Candle { get; set; }
    }
}