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
        public IActionResult Display()
        {
            var data = this._adminService.List().ToList();
            return View(data);
        }
        public IActionResult Edit(Guid id)
        {
            var data = _adminService.GetById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(ApplicationUser model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _adminService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Display));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        //[HttpPost]
        //public async  Task<IActionResult> Update(Guid id,UserUpdateRequest model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);
        //    var result = await _adminService.Update(id,model);
        //    if (result)
        //    {
        //        TempData["msg"] = "Added Successfully";
        //        return RedirectToAction(nameof(Display));
        //    }
        //    else
        //    {
        //        TempData["msg"] = "Error on server side";
        //        return View(model);
        //    }
        //}

    }
}
