using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _authService;
        public AdminController(IAdminService authService)
        {
            this._authService = authService;
        }
        public IActionResult Display()
        {
            var data = this._authService.List().ToList();
            return View(data);
        }
        public IActionResult UserList()
        {
            var data = this._authService.List().ToList();
            return View(data);
        }

    }
}
