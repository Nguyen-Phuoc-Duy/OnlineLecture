using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Models.Domain;
using System.Security.Claims;

namespace OnlineLecture.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public DashboardController( UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Display()
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
    }
}
