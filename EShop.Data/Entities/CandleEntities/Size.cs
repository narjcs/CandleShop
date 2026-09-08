using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.CandleEntities;
public class Size : BaseEntity
{
    public string Title { get; set; }
    public ICollection<CandleDetail> CandleDetails { get; set; }
}