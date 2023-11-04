using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Superadmin, Admin, HR")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.IndexGetAllAsync());
        }

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            var isSucceded = await _blogService.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var newmodel = await _blogService.GetUpdateAsync(id);
            if (newmodel is null) return NotFound();

            return View(newmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogUpdateVM model)
        {
            var isSucceded = await _blogService.PostUpdateAsync(id, model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            model = await _blogService.GetUpdateAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _blogService.DeleteAsync(id);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _blogService.DetailsAsync(id);
            if (model is null) return NotFound();
            
            return View(model);
        }
    }
}
