using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
