using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class Candle : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public string MainImageName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        #endregion

        #region Relations
        public ICollection<SelectedCategory> SelectedCategories { get; set; }
        public ICollection<CandleDetail> CandleDetails { get; set; }
        public ICollection<Gallery> Galleries { get; set; }
        #endregion
    }
}
