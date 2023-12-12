using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using WebsitePortUTC2.Services;

namespace WebsitePortUTC2.Controllers
{
    public class AdmissionsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ISchoolService _schoolService;

        public AdmissionsController(INewsService newsService, ISchoolService schoolService)
        {
            _newsService = newsService;
            _schoolService = schoolService;
        }
        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                #region data 4 news
                var news = await _newsService.GetListNewsByPaging(null, null, 1, null);
                // Thực hiện các thao tác với dữ liệu provinces
                ViewBag.news = news.data;
                #endregion

                #region data school
                var school = await _schoolService.GetSchoolAsync();
                ViewBag.school = school;
                #endregion
                ViewBag.PageSize = 10;
                return View();
            }
            catch (Exception ex)
            {
                // Xử lý exception, có thể ghi log, hiển thị thông báo lỗi, vv.
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetNewsListByPaging(int? page, int? filter, int? record)
        {
            var pageNumber = page ?? 1;
            var pageSize = record ?? 3;
            var pageListNews = await _newsService.Paging(filter, null, pageNumber, pageSize);
            return Json(pageListNews);
        }
    }
}
