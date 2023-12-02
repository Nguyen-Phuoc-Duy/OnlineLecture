using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OnlineLecture.Models;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;
using OnlineLecture.Repositories.Implementation;
using System.Diagnostics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineLecture.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DatabaseContext _context;
        private readonly IUserSubject _userSubjectService;

        public HomeController(ISubjectService service, UserManager<ApplicationUser> userManager, DatabaseContext context,IUserSubject userSubjectService)
        {
            this._subjectService = service;
            this.userManager = userManager;
            this._context = context;
            this._userSubjectService = userSubjectService;
        }
        public IActionResult Index(string searchString)
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

                }
                if (string.IsNullOrEmpty(searchString))
                {
                    var query = $"SELECT SubjectModel.* " +
                        $"FROM SubjectModel " +
                        $"LEFT JOIN UserSubjectModel ON SubjectModel.IdSubject = UserSubjectModel.IdSubject " +
                        $"AND UserSubjectModel.IdUser = '{user.Id}'" +
                        $"WHERE UserSubjectModel.IdSubject IS NULL;";
                    var data = _context.SubjectModel.FromSqlRaw(query).ToList();
                    return View(data);
                }
                else
                {
                    IQueryable<SubjectModel> data = _context.SubjectModel;

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        data = data.Where(p => p.NameSubject.Contains(searchString));
                    }
                    ViewBag.SearchString = searchString;
                    if (data.IsNullOrEmpty())
                    {
                        ViewBag.ErrorMessage = "No results for this research!";

                    }
                    return View(data);

                }
            }


            return View();
        }

       
        public IActionResult AddTheClass(int IdSubject)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(userId))
            {

                var user = userManager.FindByIdAsync(userId).Result;


                if (user != null)
                {
                    var userSubjectModel = new UserSubjectModel
                    {
                        IdUser = user.Id,
                        IdSubject = IdSubject
                    };
                    var res = _userSubjectService.AddUserSubject(userSubjectModel);
                    if (res)
                    {
                        return RedirectToAction("Index");
                    }
                   

                }
            }
            return RedirectToAction("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}