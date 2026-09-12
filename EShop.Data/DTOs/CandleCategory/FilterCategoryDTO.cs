using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.CandleEntities;

namespace EShop.Data.DTOs.CandleCategory
{
    public class FilterCategoryDTO : BasePaging
    {
        public long? CategoryId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public FilterCategoryStatus CategoryStatus { get; set; }
        public List<Category> Data { get; set; }

        #region Methods
        public FilterCategoryDTO SetData(List<Category> data)
        {
            Data = data;
            return this;
        }
        public FilterCategoryDTO SetPaging(BasePaging paging)
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
    public enum FilterCategoryStatus
    {
        All,
        Active,
        DeActive
    }
}