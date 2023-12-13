using Microsoft.AspNetCore.Mvc;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Route("admin")]
    //[Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        //[Route("")]
        [Route("HomeAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
