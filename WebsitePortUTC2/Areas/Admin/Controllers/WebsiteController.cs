using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WebsitePortUTC2.Services;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    public class WebsiteController : Controller
    {
        private readonly ISchoolService _schoolService;
        public WebsiteController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [Route("admin/Website/InformationWebsite")]
        public async Task<IActionResult> InformationWebsite()
        {
            var school = await _schoolService.GetSchoolAsync();
            ViewBag.School = school;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int schoolId, string name, string shortName, string code, string logoUrl, string faviconUrl, int addressId, string hotline, string phone, string email)
        {
            var res = await _schoolService.PutSchool(schoolId, name, shortName, code, logoUrl, faviconUrl, hotline, phone, email);
            return RedirectToAction("InformationWebsite");
        }
    }
}
