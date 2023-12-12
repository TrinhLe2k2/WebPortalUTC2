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
        private readonly IImageService _imageService;
        public NewsController(INewsService newsService, ISchoolService schoolService, IImageService imageService)
        {
            _newsService = newsService;
            _schoolService = schoolService;
            _imageService = imageService;
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
        public async Task<IActionResult> Create(string name, string description, int newsCategoryId, string metaUrl, IFormFile poster)
        {
            var postimg = 0;
            if (poster != null)
            {
                var respostimg = await _imageService.PostImage("poster", poster);
                postimg = respostimg.data.id;
            }
            if(postimg == 0)
            {
                var result = await _newsService.PostNews(name, description, newsCategoryId, metaUrl, null);
            }
            else
            {
                var result = await _newsService.PostNews(name, description, newsCategoryId, metaUrl, postimg);
            }
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
        public async Task<IActionResult> Edit(int newsId, string name, string description, int imageId, int newsCategoryId, string metaUrl, string publishedAt)
        {
            var res = await _newsService.PutNews(newsId, name, description, imageId, newsCategoryId, metaUrl, publishedAt);
            if(res.data != null)
            {
                return RedirectToAction("NewsList");
            }
            return View(newsId);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _newsService.DeleteNews(id);
            var resultJson = new {
                StatusCode = 200,
            };
            return Json(resultJson);
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                #region data 4 news
                var news = await _newsService.GetAllNews();
                // Thực hiện các thao tác với dữ liệu provinces
                ViewBag.news = news;
                ViewBag.newsCount = news.Count < 10 ? news.Count : 10;
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
