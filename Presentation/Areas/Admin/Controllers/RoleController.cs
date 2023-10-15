using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Role;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Superadmin")]

    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _roleService.IndexGetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateVM model)
        {
            var isSucceded = await _roleService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var newmodel = await _roleService.GetUpdateAsync(id);
            if (newmodel != null) return NotFound();    

            return View(newmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, RoleUpdateVM model)
        {
            var isSucceded = await _roleService.PostUpdateAsync(id, model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var isSucceded = await _roleService.DeleteAsync(id);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return NotFound();
        } 
    }
}
