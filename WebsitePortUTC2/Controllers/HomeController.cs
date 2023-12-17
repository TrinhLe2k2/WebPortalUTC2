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
        //[Route("Home")]
        public async Task<IActionResult> Index()
        {
            try
            {
                #region data 4 news
                var news = await _newsService.GetListNewsByPaging(null, null, 1, 4);
                if(news != null)
                {
                    ViewBag.FourNews = (news.data.Count < 4) ? news.data.Count : 4;
                    ViewBag.news = news.data;
                }
                else
                {
                    ViewBag.FourNews = 0;
                    ViewBag.news = null;
                }
                
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

        [Route("Introduction")]
        public async Task<IActionResult> Introduction()
        {
            try
            {
                #region data 4 news
                var news = await _newsService.GetListNewsByPaging(null, null, 1, 4);
                if (news != null)
                {
                    ViewBag.FourNews = (news.data.Count < 4) ? news.data.Count : 4;
                    ViewBag.news = news.data;
                }
                else
                {
                    ViewBag.FourNews = 0;
                    ViewBag.news = null;
                }
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
            if(HttpContext.Session.GetString("UserName") == null)
            {
                if (email == "email@example.com" && pass == "123")
                {
                    HttpContext.Session.SetString("UserName", email);
                    return Redirect("/HomeAdmin");
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
