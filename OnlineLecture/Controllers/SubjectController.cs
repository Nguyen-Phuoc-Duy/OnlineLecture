using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Controllers
{
    public class SubjectController : Controller
    {

        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService service)
        {
            this._subjectService = service;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SubjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var res = _subjectService.AddSubject(model);
            if (res)
            {
                TempData["msg"] = "Added successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var record = _subjectService.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(SubjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var res = _subjectService.UpdateSubject(model);
            if (res)
            {
                TempData["msg"] = "Updated successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

     
        public IActionResult Delete(int id)
        {
            var res = _subjectService.DeleteSubject(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = _subjectService.GetAll();
            return View(data);
        }
    }
}
