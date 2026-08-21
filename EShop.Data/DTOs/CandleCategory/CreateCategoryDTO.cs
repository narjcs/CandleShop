namespace EShop.Data.DTOs.CandleCategory
{
    public class CreateCategoryDTO
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}
