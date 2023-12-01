using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Controllers
{
    public class UserSubjectController : Controller
    {
        private readonly IUserSubject _userSubjectService;

        public UserSubjectController(IUserSubject service)
        {
            this._userSubjectService = service;
        }
    }
}
