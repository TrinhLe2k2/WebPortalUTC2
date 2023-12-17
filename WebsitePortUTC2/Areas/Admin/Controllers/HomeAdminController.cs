using Microsoft.AspNetCore.Mvc;
using WebsitePortUTC2.Models.Authentication;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Route("admin")]
    //[Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        //[Route("")]
        [Route("HomeAdmin")]
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
    }
}
