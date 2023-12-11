using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebsitePortUTC2.Services;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
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
    }
}
