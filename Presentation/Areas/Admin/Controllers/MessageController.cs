using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Message;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin, Admin, HR")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _messageService.GetAllMessages());
        }
    }
}
