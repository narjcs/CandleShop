using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class SiteFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteFooter");
        }
    }
}
