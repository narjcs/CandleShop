namespace EShop.Data.DTOs.Candle
{
    public class EditCandleDetailDTO
    {
        public long DetailId { get; set; }
        public long ColorId { get; set; }
        public long ScentId { get; set; }
        public long SizeId { get; set; }
        public string SizeValue { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }
    }
}
