using EShop.Data.Entities.Common;
using EShop.Data.Entities.CandleEntities;

namespace EShop.Data.Entities.OrderEntities
{
    public class OrderDetail : BaseEntity
    {
        public long CandleId { get; set; }
        public Candle Candle { get; set; }
    }
}
