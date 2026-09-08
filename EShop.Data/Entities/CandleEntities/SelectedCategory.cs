using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.CandleEntities;
public class SelectedCategory : BaseEntity
{
    public long CandleId { get; set; }
    public long CategoryId { get; set; }
    public Candle Candle { get; set; }
    public Category Category { get; set; }
}