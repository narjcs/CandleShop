namespace EShop.Application.Utils
{
    public static class PathExtension
    {
        #region CandleImage
        public static string CandleImage = "/content/images/CandleImage/origin/";

        public static string CandleImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/CandleImage/origin/");

        public static string CandleImageThumb = "/content/images/CandleImage/thumb/";

        public static string CandleImageThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/CandleImage/thumb/");
        #endregion

        #region Category
        public static string CategoryImage = "/content/images/Category/origin/";

        public static string CategoryServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Category/origin/");

        public static string CategoryThumb = "/content/images/Category/thumb/";

        public static string CategoryThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Category/thumb/");
        #endregion

        #region CandleGallery
        public static string CandleGalleryImage = "/content/images/CandleGallery/origin/";

        public static string CandleGalleryServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/CandleGallery/origin/");

        public static string CandleGalleryThumb = "/content/images/CandleGallery/thumb/";

        public static string CandleGalleryThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/CandleGallery/thumb/");
        #endregion

        #region Banner
        public static string BannerImage = "/content/images/Banner/origin/";

        public static string BannerServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Banner/origin/");

        public static string BannerThumb = "/content/images/Banner/thumb/";

        public static string BannerThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Banner/thumb/");
        #endregion
    }
}