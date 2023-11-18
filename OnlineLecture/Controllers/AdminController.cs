using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Models.Domain;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            this._adminService = adminService;
        }
        public IActionResult ViewAllUsers()
        {
            var data = this._adminService.List().ToList();
            return View(data);
        }
        public IActionResult Update(Guid id)
        {
            var data = _adminService.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUser model)
        {
           
            if (!ModelState.IsValid)
                return View(model);

            var result = await _adminService.UpdateUser(model.Id.ToString(), model);

            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(nameof(ViewAllUsers));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

    }
}
