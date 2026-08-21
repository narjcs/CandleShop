using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.Candle;

public class Gallery : BaseEntity
{
    public long CandleId { get; set; }
    public string ImageName { get; set; }
    public int Order { get; set; }
    public Candle Candle { get; set; }
}