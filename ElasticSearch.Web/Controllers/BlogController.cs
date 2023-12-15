using ElasticSearch.Web.Services;
using ElasticSearch.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService; 
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Search()
        {
            return View(await _blogService.SearchAsync(null));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        { 
            ViewBag.SearchText = searchText;    
            return View(await _blogService.SearchAsync(searchText));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogCreateViewModel model)
        {
            var isSucces = await _blogService.SaveAsync(model);
            if (isSucces)
            {
                TempData["result"] = "Kayıt Başarılı";
                return RedirectToAction(nameof(BlogController.Save));
            }

            TempData["result"] = "Kayıt Başarısız";
            return RedirectToAction(nameof(BlogController.Save));

        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var isSucces = await _blogService.UpdateAsync("fdgd",new Models.Blog());
           
            return null;
        }
    }
}
