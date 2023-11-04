using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Message;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMessageService _messageService;

        public ContactController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(MessageVM model)
        {
            var isSucceded = await _messageService.CreateAsync(model, User);
            if (isSucceded) return View(); 

            //return RedirectToAction(nameof(Index), model);
            return View(model);
        }
    }
}
