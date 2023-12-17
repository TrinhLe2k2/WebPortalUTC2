using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebsitePortUTC2.Models.Authentication;
using WebsitePortUTC2.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NewsController : Controller
    {
        private readonly ISchoolService _schoolService;
        private readonly INewsService _newsService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        public NewsController(INewsService newsService, ISchoolService schoolService, IImageService imageService , ICategoryService categoryService)
        {
            _newsService = newsService;
            _schoolService = schoolService;
            _imageService = imageService;
            _categoryService = categoryService;
        }

        [Route("news/newslist")]
        [Authentication]
        public IActionResult NewsList()
        {
            return View();
        }

        [Route("news/create")]
        [Authentication]
        public async Task<IActionResult> Create()
        {
            #region GetCategoriesListByStatus 
            var categories = await _categoryService.GetListByStatus(1);
            ViewBag.Categories = categories;
            ViewBag.CategoriesCount = categories.Count;
            #endregion
            return View();
        }

        [HttpPost]
        [Authentication]
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
                if(result.error?.code != null && result.error.code.ToString().StartsWith("2"))
                {
                    TempData["Message"] = "Post: Success";
                }
                else
                {
                    TempData["Message"] = "Post: Error";
                }
            }
            else
            {
                var result = await _newsService.PostNews(name, description, newsCategoryId, metaUrl, postimg);
                if (result.error?.code != null && result.error.code.ToString().StartsWith("2"))
                {
                    TempData["Message"] = "Post: Success";
                }
                else
                {
                    TempData["Message"] = "Post: Error";
                }
            }
            return RedirectToAction("NewsList");
        }

        [Route("news/edit/{id:int}")]
        [Authentication]
        public async Task<IActionResult> Edit(int id)
        {
            #region GetCategoriesListByStatus 
            var categories = await _categoryService.GetListByStatus(1);
            ViewBag.Categories = categories;
            ViewBag.CategoriesCount = categories.Count;
            #endregion

            var isNews = await _newsService.GetNewsByID(id);
            if(isNews == null)
            {
                return RedirectToAction("NewsList");
            }
            ViewBag.newsCategoryId = isNews.newsCategoryId;
            return View(isNews);
        }
        
        [HttpPost]
        [Authentication]
        public async Task<IActionResult> Edit(int newsId, string name, string description, int imageId, int newsCategoryId, string metaUrl, string publishedAt, IFormFile newPoster)
        {
            var newImgId = -1;
            if (newPoster != null)
            {
                var respostimg = await _imageService.PostImage("poster", newPoster);
                newImgId = respostimg.data.id;
            }
            if(newImgId == -1)
            {
                var res = await _newsService.PutNews(newsId, name, description, imageId, newsCategoryId, metaUrl, publishedAt);
                
                if (res.error?.code != null && res.error.code.ToString().StartsWith("2"))
                {
                    TempData["Message"] = "Update: Success";
                    return RedirectToAction("NewsList");
                }
                else
                {
                    TempData["Message"] = "Update: Error";
                    return RedirectToAction("Edit", new { id = newsId });
                }
            }
            else
            {
                var res = await _newsService.PutNews(newsId, name, description, newImgId, newsCategoryId, metaUrl, publishedAt);
                
                if (res.error?.code != null && res.error.code.ToString().StartsWith("2"))
                {
                    TempData["Message"] = "Update: Success";
                    return RedirectToAction("NewsList");
                }
                else
                {
                    TempData["Message"] = "Update: Error";
                    return RedirectToAction("Edit", new { id = newsId });
                }
            }
        }

        [Authentication]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _newsService.DeleteNews(id);
            var resultJson = new {
                StatusCode = 200,
            };
            return Json(resultJson);
        }

        [Route("News/{slug}-{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            #region data 4 news
            var news = await _newsService.GetListNewsByPaging(null, null, 1, 10);
            if (news != null)
            {
                ViewBag.newsCount = news.data.Count < 10 ? news.data.Count : 10;
                ViewBag.FourNews = (news.data.Count < 4) ? news.data.Count : 4;
                ViewBag.news = news.data;
            }
            else
            {
                ViewBag.newsCount = 0;
                ViewBag.FourNews = 0;
                ViewBag.news = null;
            }
            #endregion

            #region data school
            var school = await _schoolService.GetSchoolAsync();
            ViewBag.school = school;
            #endregion

            var detail = await _newsService.GetNewsByID(id);
            if (detail.status == 1)
            {
                ViewBag.detail = detail;
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            string url = Url.Action("Index", "Admissions", new { txtSearch = search });
            return Redirect(url);
        }

    }
}
