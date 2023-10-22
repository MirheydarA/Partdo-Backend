using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class OrderTrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
