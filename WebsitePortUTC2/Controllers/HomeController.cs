using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebsitePortUTC2.Models;
using WebsitePortUTC2.Services;

namespace WebsitePortUTC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ISchoolService _schoolService;

        public HomeController(INewsService newsService, ISchoolService schoolService)
        {
            _newsService = newsService;
            _schoolService = schoolService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                #region data 4 news
                var news = await _newsService.GetAllNews();
                // Thực hiện các thao tác với dữ liệu provinces
                ViewBag.news = news;
                #endregion

                #region data school
                var school = await _schoolService.GetSchoolAsync();
                ViewBag.school = school;
                #endregion
                return View();
            }
            catch (Exception ex)
            {
                // Xử lý exception, có thể ghi log, hiển thị thông báo lỗi, vv.
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> Introduction()
        {
            try
            {
                #region data 4 news
                var news = await _newsService.GetAllNews();
                // Thực hiện các thao tác với dữ liệu provinces
                ViewBag.news = news;
                #endregion

                #region data school
                var school = await _schoolService.GetSchoolAsync();
                ViewBag.school = school;
                #endregion
                return View();
            }
            catch (Exception ex)
            {
                // Xử lý exception, có thể ghi log, hiển thị thông báo lỗi, vv.
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            if(email == "email@example.com" && pass == "123")
            {
                return Redirect("/Admin/HomeAdmin/Index");
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
