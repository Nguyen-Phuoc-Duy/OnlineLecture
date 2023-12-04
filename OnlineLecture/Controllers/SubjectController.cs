using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Controllers
{
    [Authorize(Roles = "admin")]
    public class SubjectController : Controller
    {

        private readonly ISubjectService _subjectService;
        private readonly ILectureService _lectureService;
        private readonly DatabaseContext _context;

        public SubjectController(ISubjectService service, DatabaseContext context, ILectureService lectureService)
        {
            this._subjectService = service;
            this._context = context;
            this._lectureService = lectureService;
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
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var query = $"select SubjectLectureModel.* from SubjectLectureModel " +
                $"INNER JOIN SubjectModel on SubjectLectureModel.IdSubject = SubjectModel.IdSubject " +
                $"where SubjectModel.IdSubject = '{id}'";
            var data = _context.SubjectLectureModel.FromSqlRaw(query).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                var idLecture = data[i].IdLecture;
                _lectureService.DeleteLecture(idLecture);
                var idSubjectLecture = data[i].IdSubjectLecture;
                var subjectLecture = _context.SubjectLectureModel.Find(idSubjectLecture);
                _context.SubjectLectureModel.Remove(subjectLecture);
                _context.SaveChanges();
            }
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
