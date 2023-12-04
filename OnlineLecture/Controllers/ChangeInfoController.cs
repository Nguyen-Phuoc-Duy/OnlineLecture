using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Models.Domain;
using OnlineLecture.Repositories.Abstract;
using System.Security.Claims;

namespace OnlineLecture.Controllers
{
    public class ChangeInfoController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdminService _adminService;
        public ChangeInfoController(UserManager<ApplicationUser> userManager, IAdminService adminService)
        {
            this.userManager = userManager;
            this._adminService = adminService;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(userId))
            {

                var user = userManager.FindByIdAsync(userId).Result;


                if (user != null)
                {
                    ViewData["UserId"] = user.Id;
                    ViewData["Username"] = user.UserName;
                    ViewData["Email"] = user.Email;
                    ViewData["Name"] = user.Name;
                    return View();
                }
            }

            // Nếu không đăng nhập hoặc không có ID, có thể xử lý theo ý muốn
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(ApplicationUser model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var result = await _adminService.UpdateUser(model.Id.ToString(), model);

            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
    }
}
