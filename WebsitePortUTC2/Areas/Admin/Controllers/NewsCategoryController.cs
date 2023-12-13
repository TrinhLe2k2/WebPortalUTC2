using Microsoft.AspNetCore.Mvc;
using WebsitePortUTC2.Services;

namespace WebsitePortUTC2.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NewsCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public NewsCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("NewsCategory/newsCategorylist")]
        public IActionResult NewsCategoryList()
        {
            var newsCategoryList = _categoryService.GetCategoryById(1);
            ViewBag.NewsCategoryList = newsCategoryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var result = await _categoryService.PostCategory(name);
            return RedirectToAction("NewsCategoryList");
        }

        [Route("NewsCategory/edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var isNewsCategory = await _categoryService.GetCategoryById(id);
            if (isNewsCategory == null)
            {
                return RedirectToAction("NewsCategoryList");
            }
            return View(isNewsCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int categoryId, string name, int status)
        {
            var res = await _categoryService.PutCategory(categoryId, name, status);
            if (res.data != null)
            {
                return RedirectToAction("NewsCategoryList");
            }
            return View(categoryId);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _categoryService.DeleteCategory(id);
            return RedirectToAction("NewsCategoryList");
        }

    }
}
