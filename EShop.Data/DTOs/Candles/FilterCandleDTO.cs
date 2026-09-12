using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.CandleEntities;
using System.ComponentModel.DataAnnotations;

namespace EShop.Data.DTOs.Candles
{
    public class FilterCandleDTO : BasePaging
    {
        public string Title { get; set; }
        public long? CategoryId { get; set; }
        public long? ColorId { get; set; }
        public long? ScentId { get; set; }
        public long? SizeId { get; set; }
        public int? MostPrice { get; set; }
        public int? LeastPrice { get; set; }
        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; }
        public FilterCandleStatus CandleStatus { get; set; }
        public FilterCandleOrder CandleOrder { get; set; }
        public List<Candle> Data { get; set; }

        #region Methods
        public FilterCandleDTO SetData(List<Candle> data)
        {
            Data = data;
            return this;
        }
        public FilterCandleDTO SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;
            AllEntitiesCount = paging.AllEntitiesCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            TakeEntity = paging.TakeEntity;
            SkipEntity = paging.SkipEntity;
            PageCount = paging.PageCount;
            return this;
        }
        #endregion
    }

    public enum FilterCandleStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Available,
        [Display(Name = "غیرفعال")]
        NotAvailable,
        [Display(Name = "موجود")]
        HasStockCount,
        [Display(Name = "ناموجود")]
        HasZeroStockCount
    }

    public enum FilterCandleOrder
    {
        [Display(Name = "جدیدترین")]
        Newest,
        [Display(Name = "قدیمی‌ترین")]
        Oldest,
        [Display(Name = "گران‌ترین")]
        MostExpensive,
        [Display(Name = "ارزان‌ترین")]
        Cheapest
    }
}
