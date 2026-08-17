using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; } // when each page has specefic URL like .../candles/glass
        public bool IsActive { get; set; }
        public int Order { get; set; }

        public ICollection<SelectedCategory> SelectedCategories { get; set; }
    }
}