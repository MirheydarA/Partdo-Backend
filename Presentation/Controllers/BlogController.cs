using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Blog;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> Index(BlogIndexVM model)
        {
            return View(await _blogService.IndexGetAllAsync());
        }

        public async Task<IActionResult> Details(int id) 
        {
            var listBlogs = await _blogService.IndexGetAllAsync();
            ViewBag.Listblog = listBlogs;
            var model = await _blogService.DetailsAsync(id);
            if (model is not null) return View(model);

            return RedirectToAction(nameof(Index), "NotFound");
        }
    }
}
