using EShop.Data.DTOs.CandleCategory;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.CandleEntities;

namespace EShop.Data.DTOs.Candles
{
    public class FilterColorDTO : BasePaging
    {
        public string Title { get; set; }
        public List<Color> Data { get; set; }

        #region Methods
        public FilterColorDTO SetData(List<Color> data)
        {
            Data = data;
            return this;
        }
        public FilterColorDTO SetPaging(BasePaging paging)
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
}
