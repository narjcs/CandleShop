using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.Candle;

public class CandleDetail : BaseEntity
{
    public long CandleId { get; set; }

    public long ColorId { get; set; }
    public long ScentId { get; set; }
    public long SizeId { get; set; } // Keep the Id

    public string SizeValue { get; set; } // Keep the true value

    public int Price { get; set; }
    public int StockCount { get; set; }

    // Navigation Properties: IDs identify the related entities; these properties provide access to the related entity data.
    public Candle Candle { get; set; }
    public Color Color { get; set; }
    public Scent Scent { get; set; }
    public Size Size { get; set; }
}