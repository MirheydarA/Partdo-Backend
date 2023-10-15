using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
