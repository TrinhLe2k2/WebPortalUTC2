using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebsitePortUTC2.Services;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NewsController : Controller
    {
        private readonly ISchoolService _schoolService;
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService, ISchoolService schoolService)
        {
            _newsService = newsService;
            _schoolService = schoolService;
        }

        [Route("admin/news/newslist")]
        public IActionResult NewsList()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string description, int newsCategoryId, string metaUrl)
        {
            var result = await _newsService.PostNews(name, description, newsCategoryId, metaUrl);
            return RedirectToAction("NewsList");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var isNews = await _newsService.GetNewsByID(id);
            if(isNews == null)
            {
                return RedirectToAction("NewsList");
            }
            return View(isNews);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int newsId, string name, string description, int newsCategoryId, string metaUrl, string publishedAt)
        {
            var res = await _newsService.PutNews(newsId, name, description, newsCategoryId, metaUrl, publishedAt);
            if(res.data != null)
            {
                return RedirectToAction("NewsList");
            }
            return View(newsId);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _newsService.DeleteNews(id);
            return RedirectToAction("NewsList");
        }

        public async Task<IActionResult> Detail(int id)
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

                var detail = await _newsService.GetNewsByID(id);
                ViewBag.detail = detail;
                return View();
            }
            catch (Exception ex)
            {
                // Xử lý exception, có thể ghi log, hiển thị thông báo lỗi, vv.
                return RedirectToAction("Error");
            }
        }
    }
}
