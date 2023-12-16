using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using WebsitePortUTC2.Services;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    public class WebsiteController : Controller
    {
        private readonly ISchoolService _schoolService; 
        private readonly IAddressService _addressService; 
        public WebsiteController(ISchoolService schoolService, IAddressService addressService)
        {
            _schoolService = schoolService;
            _addressService = addressService;
        }

        [Route("Website")]
        public async Task<IActionResult> InformationWebsite()
        {
            var school = await _schoolService.GetSchoolAsync();
            ViewBag.School = school;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int schoolId, string name, string shortName, string code, string logoUrl, string faviconUrl, string addressText, string hotline, string phone, string email)
        {
            var res = await _schoolService.PutSchool(schoolId, name, shortName, code, logoUrl, faviconUrl, hotline, phone, email);
            if (addressText != null && addressText != "")
            {
                var putAddress = await _addressService.PutAddress(addressText);
            }
            if (res.error.code == 200)
            {
                TempData["SuccessMessage"] = "Success";
            }
            else
            {
                TempData["ErrorMessage"] = "Error";
            }
            return RedirectToAction("InformationWebsite");
        }
    }
}
